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
    
    [SerializeField] Rig handIK;
    [SerializeField] TwoBoneIKConstraint rHandIK;
    [SerializeField] TwoBoneIKConstraint lHandIK;
    
    // --------- pistol -------------
    [SerializeField] AnimationClip equipAnim_pistol;
    float animLen_equipPistol;

    // ---------- rifle ---------------
    [SerializeField] AnimationClip equipAnim_rifle;
    float animLen_equipRifle;


    //============================================
    void Awake()
    {
        animLen_equipPistol = equipAnim_pistol.length;  // equip, holster 는 순서만 반대기 때문에 길이가 같음. 
        animLen_equipRifle = equipAnim_rifle.length;

    }



    //===========================================
    

    //================= 권총 ===========================

    public void OnHoldStart(WeaponSlot weaponSlot)
    {
        DOTween.Sequence()
        .AppendInterval(0.01f)
        .AppendCallback( ()=>        
            {
                handIK.weight = 0f; //처음에 hand IK를 꺼놓고, 다음 스테이트에서 켜짐.
                // 그리고 Multi parent Constraint 조절해야함. 


                
             
                // Debug.Log($"[Anim] Set Hold Start {isEquiping} {animLen_equipPistol}");         
            }        
        );
        // .Append( DOTween.To(()=>lHandIK.weight, x=> lHandIK.weight = x , isEquiping?1:0,  animLen_equipPistol )); // 왼손 가중치 설정 
    }


    public void OnPistolAnim(bool isEquiping)
    {
        DOTween.Sequence()
        .AppendInterval(0.01f)
        .AppendCallback( ()=>        
            {
                handIK.weight = 1f;
                rHandIK.weight = 1f;
                lHandIK.weight = isEquiping?0:1;    //시작 ik weight 설정 
             
                Debug.Log($"[Anim] Pistol {isEquiping}  {animLen_equipPistol}");         
            }        
        )
        .Append( DOTween.To(()=>lHandIK.weight, x=> lHandIK.weight = x , isEquiping?1:0,  animLen_equipPistol )); // 왼손 가중치 설정 
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
            
    
                Debug.Log($"[Anim] Rifle {isEquiping} {animLen_equipRifle}");         
            }        
        )
        .Append( DOTween.To(()=>lHandIK.weight, x=> lHandIK.weight = x , isEquiping?1:0,  animLen_equipRifle  ));// 왼손 가중치 설정 
        
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
