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
    
    //
    [SerializeField] AnimationClip equipAnim_pistol;
    [SerializeField] AnimationClip holsterAnim_pistol;
    float animLen_holsterPistol;


    //============================================
    void Awake()
    {
        animLen_holsterPistol = holsterAnim_pistol.length;  // equip, holster 는 순서만 반대기 때문에 길이가 같음. 

    }

    void Start()
    {
    }


    //===========================================
    

    //================= 권총 ===========================

    public void OnPistolAnim(bool isEquiping)
    {
        StartCoroutine(C_onPistolAnim(isEquiping));
    }


    // ====== Equip
    IEnumerator C_onPistolAnim(bool isEquiping)
    {        
        yield return null;
        handIK.weight = 1f;
        rHandIK.weight = 1f;
        lHandIK.weight = isEquiping?0:1;    //시작 ik weight 설정 
        
    
        DOTween.To(()=>lHandIK.weight, x=> lHandIK.weight = x , isEquiping?1:0, animLen_holsterPistol );    // 왼손은 처음에 파지안함.
        Debug.Log("[Anim] Pistol {isEquiping}");
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
