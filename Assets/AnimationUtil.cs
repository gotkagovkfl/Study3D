using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class AnimationUtil : MonoBehaviour
{
    
    [SerializeField] Rig handIK;
    public void Activate_HandIK()
    {
        // handIK.weight = 1f;
        StartCoroutine(Test(1));
        Debug.Log("[Rig]handIK on");
    }

    public void Deactivate_HandIK()
    {
        // handIK.weight = 0f;
        StartCoroutine(Test(0));
        Debug.Log("[Rig]handIK off");
    }


    IEnumerator Test(float t)
    {
        yield return null;
        handIK.weight = t;
        Debug.Log($"[Rig]handIK = {t}");
        
    }


}
