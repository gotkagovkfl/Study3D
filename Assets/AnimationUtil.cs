using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using DG.Tweening;

/// <summary>
/// RIG Weight 를 키프레임 애니메이션으로 직접 조정할 수 없어서, 이런식으로 해당 키프레임 때 함수를 지연호출하는 '꼼수'를 사용하여 rig weight 조정함. 
///    DOTWEEN 은 값을 자연스럽게 수정하기 위해 사용하였음. 
/// </summary>
public class AnimationUtil : MonoBehaviour
{
    EquipmentSlot equipmentSlot;
    
    Dictionary<WeaponType, MultiParentConstraint> mpcs;
    

    [SerializeField] Rig handIK;
    [SerializeField] TwoBoneIKConstraint rHandIK;
    [SerializeField] TwoBoneIKConstraint lHandIK;

    
    // ---------- rifle ---------------
    [SerializeField] AnimationClip holdAnim_rifle;
    float animLen_hold_rifle;
    float len_holdStart_rifle = 0.25f;  // 
    
    // --------- pistol -------------
    [SerializeField] AnimationClip holdAnim_pistol;
    float animLen_hold_pistol;
    float len_holdStart_pistol;



    //============================================
    void Awake()
    {
        //
        equipmentSlot = GetComponentInParent<EquipmentSlot>();
        mpcs= new();
        foreach(var kv in equipmentSlot.weaponSlots)
        {
            WeaponType weaponType = kv.Key;
            MultiParentConstraint mpc = kv.Value?.GetComponent<MultiParentConstraint>();     // 항상 붙어 있을거임. 
            
            mpcs.Add(weaponType,mpc);
        }
        

        //
        animLen_hold_pistol = holdAnim_pistol.length;  // equip, holster 는 순서만 반대기 때문에 길이가 같음. 
        animLen_hold_rifle = holdAnim_rifle.length;

        
        //------------------------------

    }



    //===========================================
    

    //================= 권총 ===========================

    public void OnPistolAnim(bool isEquiping)
    {
        DOTween.Sequence()
        .AppendInterval(0.01f)
        .AppendCallback( ()=>        
            {
                handIK.weight = 1f;
                rHandIK.weight = 1f;
                lHandIK.weight = isEquiping?0:1;    //시작 ik weight 설정 
             
                Debug.Log($"[Anim] Pistol {isEquiping}  {animLen_hold_pistol}");         
            }        
        )
        .Append( DOTween.To(()=>lHandIK.weight, x=> lHandIK.weight = x , isEquiping?1:0,  animLen_hold_pistol )); // 왼손 가중치 설정 
    }


    //================= rifle ===========================

    public void OnRifleAnim(bool isEquiping)
    {
        DOTween.Sequence()
        .AppendInterval(0.01f)
        .AppendCallback( ()=>        
            {
                handIK.weight = 1f;
                rHandIK.weight = 1f;
                lHandIK.weight = isEquiping?0:1;    //시작 ik weight 설정 
            
    
                Debug.Log($"[Anim] Rifle {isEquiping} {animLen_hold_rifle}");         
            }        
        )
        .Append( DOTween.To(()=>lHandIK.weight, x=> lHandIK.weight = x , isEquiping?1:0,  animLen_hold_rifle  ));// 왼손 가중치 설정 
        
    }




    /// <summary>
    /// "hold" - 해당 무기 타입에 따라 다른 설정.
    /// </summary>
    /// <param name="weaponType"></param>
    public void OnHold(WeaponType weaponType)
    {
        // 세팅
        float duration = 0f;
        float offset = 0f;
        switch (weaponType)
        {
            case WeaponType.Rifle:
                duration = animLen_hold_rifle;
                offset = len_holdStart_rifle;
                break;

            case WeaponType.Pistol:
                duration = animLen_hold_pistol;
                offset = len_holdStart_pistol;
                break;
            
        }
        
        // 우선 MPC 설정
        SetMPC(weaponType, true);

        // 시퀀스 재생
        DOTween.Sequence()
        .AppendInterval(0.01f)
        .AppendCallback( ()=>        
            {
                handIK.weight = 1f;
                lHandIK.weight = 0f;  
                rHandIK.weight = 0f;
            }        
        )
                .Append( DOTween.To(()=>rHandIK.weight, x=> rHandIK.weight = x , 1f,  offset)) // 총을 잡는 손  우선 설정 
        .Append( DOTween.To(()=>lHandIK.weight, x=> lHandIK.weight = x , 1f,  duration - offset )) // 보조하는 손 다음으로 설정 
        .Play();
       
    }

    /// <summary>
    /// "Holster" - hold의 역순. 보조손 -> 주손 -> 무기 
    /// </summary>
    /// <param name="weaponType"></param>
    public void OnHolster(WeaponType weaponType)
    {  
        // 세팅 
        float duration = 0f;
        float offset = 0f;
        switch (weaponType)
        {
            case WeaponType.Rifle:
                duration = animLen_hold_rifle;
                offset = len_holdStart_rifle;
                break;

            case WeaponType.Pistol:
                duration = animLen_hold_pistol;
                offset = len_holdStart_pistol;
                break;
            
        }

         // 시퀀스 재생
        DOTween.Sequence()
        .AppendInterval(0.01f)
        .AppendCallback( ()=>        
            {
                handIK.weight = 1f;
                lHandIK.weight = 1f;  
                rHandIK.weight = 1f;
            }        
        )
        .Append( DOTween.To(()=>lHandIK.weight, x=> lHandIK.weight = x , 0f,  duration - offset )) // 보조하는 손 우선 설정 
        .Append( DOTween.To(()=>rHandIK.weight, x=> rHandIK.weight = x , 0f,  offset)) // 총을 잡는 손 다음으로 설정 
        .AppendCallback(()=>SetMPC(weaponType, false))
        .Play();
        

        

    }



    /// <summary>
    /// Multi parent Constraint 를 설정한다. - idx 0 : weapon pivot, idx 1 : equipment slot
    /// </summary>
    /// <param name="weaponType"></param>
    /// <param name="isHold"></param>
    void SetMPC(WeaponType weaponType, bool isHold)
    {    
        // 홀딩 슬롯의 0번인덱스 값은 1, 나머지 0 
        // 나머지 슬롯 0번 인덱스 0, 나머지 1

        //multi parent constraint ( mpc ) 설정 - idx 0 : weapon pivot, idx 1 : weapon slot
        foreach(var kv in mpcs)
        {
            WeaponType wt = kv.Key;
            MultiParentConstraintData mpcData = kv.Value.data; 
             
            var sourceObjects = mpcData.sourceObjects;

            if (weaponType == wt)
            {
                sourceObjects.SetWeight(0, isHold? 1f:0f);
                sourceObjects.SetWeight(1, isHold? 0f:1f);
            }  
            else if (isHold)    // 홀드애니메이션이 아닌경우, 해당 무기가 아니라면 건들지 않음. - 무장해제를 할 떄는 모든 0번 인덱스가 0이어야함. 
            {
                sourceObjects.SetWeight(0, 0f);
                sourceObjects.SetWeight(1, 1f);
            }
            mpcData.sourceObjects = sourceObjects; //이건 왜 필요한지 모르겠네.
        }
    }




    //==============================================================




    /// <summary>
    /// weapon hand ik 를 활성화한다. - 무기를 들었을 때, 
    /// </summary>
    public void Activate_HandIK()
    {
        StartCoroutine(SetHandIKWeight(1));
    }

    /// <summary>
    /// weapon hand ik 를 비활성화한다.  - 무장해제시
    /// </summary>
    public void Deactivate_HandIK()
    {
        StartCoroutine(SetHandIKWeight(0));
    }


    /// <summary>
    /// 바로 실행하면 적용이 안되어서 한 프레임 지연 실행한다.  - Rig Layer에서 실행되는 애니메이션에 부착됨.
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    IEnumerator SetHandIKWeight(float t)
    {
        yield return null;
        handIK.weight = t;
        Debug.Log($"[Rig] handIK.Weight = {t}");        
    }

}
