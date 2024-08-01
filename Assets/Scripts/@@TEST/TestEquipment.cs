using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using ULT;
using UnityEngine.Rendering;
using UnityEditor.Animations;
using UnityEngine.Profiling;

public class TestEquipment : MonoBehaviour
{
    [SerializeField] UltimatePlayerInput playerInput;


    TestWeapon currWeapon;
    [SerializeField] Rig handIK;
    [SerializeField] Transform weaponParent;
    [SerializeField] Transform t_rHandGrip;
    [SerializeField] Transform t_lHandGrip;


    Animator animator;
    AnimatorOverrideController animator_override; 



    [SerializeField] GameObject prefab_testRifle;
    [SerializeField] GameObject prefab_testPistol;


    void Start()
    {
        playerInput = GetComponent<UltimatePlayerInput>();
        animator = GetComponent<Animator>();
        animator_override = animator.runtimeAnimatorController as AnimatorOverrideController;


        var weapon = GetComponentInChildren<TestWeapon>();
        if (weapon)
        {
            Equip(weapon);
        }
        else
        {
            handIK.weight= 0f;
            animator.SetLayerWeight(1,0f);
        }
    }

    void Update()
    {
        if (playerInput.weaponSelect_main)
        {
            TestWeapon w= Instantiate(prefab_testRifle).GetComponent<TestWeapon>();
            Equip(w);
        }
        else if (playerInput.weaponSelect_secondary)
        {
            TestWeapon w= Instantiate(prefab_testPistol).GetComponent<TestWeapon>();
            Equip(w);
        }
    }


    void Equip(TestWeapon weapon)
    {
        if (currWeapon)
        {
            Destroy(currWeapon.gameObject);
        }
        
        currWeapon = weapon;
        handIK.weight = 1f;
    
        currWeapon.transform.parent = weaponParent;
        currWeapon.transform.localPosition = Vector3.zero;
        currWeapon.transform.localRotation = Quaternion.identity;


        animator.SetLayerWeight(1,1f);
        animator_override["Anim_Weapon_None"] = currWeapon.waeponAnimation;


        Debug.Log($"무기장착 {weapon.gameObject.name}");
    }

    void UnEquip()
    {
        if (currWeapon)
        {
            Destroy(currWeapon.gameObject);
        }
    }


    //===============================================================

    /// <summary>
    /// 각 무기별로 파지 위치와 무기의 위치를 기억하기 위함. 
    /// </summary>
    [ContextMenu("Save Weapon Pos")]
    void SaveWeaponPos()
    {
        GameObjectRecorder recorder = new(gameObject);
        recorder.BindComponentsOfType<Transform>(weaponParent.gameObject,false);
        recorder.BindComponentsOfType<Transform>(t_rHandGrip.gameObject,false);
        recorder.BindComponentsOfType<Transform>(t_lHandGrip.gameObject,false);
        recorder.TakeSnapshot(0f);
        recorder.SaveToClip(currWeapon.waeponAnimation);
    }
}
