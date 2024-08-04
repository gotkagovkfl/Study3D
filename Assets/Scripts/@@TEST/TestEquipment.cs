using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using ULT;
using UnityEngine.Rendering;
using UnityEditor.Animations;
using UnityEngine.Profiling;
using System;

public class TestEquipment : MonoBehaviour
{
    [SerializeField] UltimatePlayerInput playerInput;


    [SerializeField]  TestWeapon currWeapon;
    [SerializeField] Transform weaponParent;

    // [SerializeField] Rig handIK;
    // [SerializeField] Transform t_rHandGrip;
    // [SerializeField] Transform t_lHandGrip;


    public Animator animator;
    bool holster;
    readonly int hash_holster = Animator.StringToHash("Holster");

    [SerializeField] GameObject prefab_testRifle;
    [SerializeField] GameObject prefab_testPistol;


    void Start()
    {
        playerInput = GetComponent<UltimatePlayerInput>();
        var weapon = GetComponentInChildren<TestWeapon>();
        if (weapon)
        {
            Equip(weapon);
        }
        else
        {
            UnArm();
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
        else if (playerInput.weaponSelect_melee)
        {
            Holster();  /// ttest
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log($"Holster {currWeapon?.gameObject.name}");
            UnArm();
        }

        // Debug.Log(handIK.weight);
    }


    void Equip(TestWeapon weapon)
    {
        if (currWeapon)
        {
            Destroy(currWeapon.gameObject);
        }
        holster = false;

        currWeapon = weapon;
    
        currWeapon.transform.parent = weaponParent;
        currWeapon.transform.localPosition = Vector3.zero;
        currWeapon.transform.localRotation = Quaternion.identity;

        animator.SetBool(hash_holster, holster);            // 무기를 장착하면 애니메이션 파라미터도 변경시킴   
        animator.Play($"Equip_{currWeapon.type}");

        Debug.Log($"무기장착 {weapon.gameObject.name}_{currWeapon.type}");
    }

    void UnEquip()
    {
        if (currWeapon)
        {
            Destroy(currWeapon.gameObject);
        }
    }

    void Holster()
    {
        holster = !holster;
        animator.SetBool(hash_holster, holster);        
    }

    void UnArm()
    {
        if (currWeapon)
        {
            Destroy(currWeapon.gameObject);
        }
        
        animator.Play("Unarmed");        
    }
    //===========================================


    //===============================================================

    /// <summary>
    /// 각 무기별로 파지 위치와 무기의 위치를 기억하기 위함. 
    /// </summary>
    // [ContextMenu("Save Weapon Pos")]
    // void SaveWeaponPos()
    // {
    //     GameObjectRecorder recorder = new(gameObject);
    //     recorder.BindComponentsOfType<Transform>(weaponParent.gameObject,false);
    //     recorder.BindComponentsOfType<Transform>(t_rHandGrip.gameObject,false);
    //     recorder.BindComponentsOfType<Transform>(t_lHandGrip.gameObject,false);
    //     recorder.TakeSnapshot(0f);
    //     recorder.SaveToClip(currWeapon.waeponAnimation);        // 현재 장착중인 무기 스크립트에서 무기의 애니메이션 참조를 받아 해당 파일에 저장.
    // }
}
