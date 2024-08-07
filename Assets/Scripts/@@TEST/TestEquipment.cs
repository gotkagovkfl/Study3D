using UnityEngine;
using ULT;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Animations.Rigging;
using DG.Tweening;
using Study3D;


public enum WeaponSlot
{
    Primary,
    Secondary,
    Melee,
    Support
}



public class TestEquipment : MonoBehaviour
{
    [SerializeField] UltimatePlayerInput playerInput;
    [SerializeField] EquipmentSlot equipmentSlot;

    //
    Dictionary<WeaponSlot,TestWeapon> equippedWeapon = new(){ {WeaponSlot.Primary, null}, {WeaponSlot.Secondary, null}, {WeaponSlot.Melee, null} };
    [SerializeField]  WeaponSlot holdingSlot;
    [SerializeField]  TestWeapon holdingWeapon;


    public Animator animator;
    bool holding;
    readonly int hash_holding = Animator.StringToHash("Holding");

    [SerializeField] GameObject prefab_testRifle;
    [SerializeField] GameObject prefab_testPistol;



    //======================================================================================================================

    void Start()
    {
        playerInput = GetComponent<UltimatePlayerInput>();
        equipmentSlot = GetComponent<EquipmentSlot>();
    

        // ------- 기본 무기 장착 ------------
        TestWeapon w1= Instantiate(prefab_testRifle).GetComponent<TestWeapon>();
        Equip(WeaponSlot.Primary, w1);
        
        TestWeapon w2= Instantiate(prefab_testPistol).GetComponent<TestWeapon>();
        Equip(WeaponSlot.Secondary, w2);

    
        // 처음엔 무장 X - 나중엔 
        Hold(WeaponSlot.Primary);
    }

    void Update()
    {
        if (playerInput.weaponSelect_main)
        {
            Hold(WeaponSlot.Primary);
        }
        else if (playerInput.weaponSelect_secondary)
        {
            Hold(WeaponSlot.Secondary);
        }
        else if (playerInput.weaponSelect_melee)
        {
            Holster();  /// ttest
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log($"Holster {holdingWeapon?.gameObject.name}");
            UnArm();
        }

        // Debug.Log(handIK.weight);
    }


    //=======================================================================================

    /// <summary>
    /// weaponSlot 에 weapon을 장착한다. 
    /// </summary>
    /// <param name="weaponSlot"></param>
    /// <param name="weapon"></param>
    void Equip(WeaponSlot weaponSlot, TestWeapon weapon)
    {
        // 기존 무기 파괴
        UnEquip(weaponSlot);

        // 새 무기 장착
        equippedWeapon[weaponSlot] = weapon;
        Transform t_weaponSlot = equipmentSlot.weaponSlots[weapon.type];
        weapon.transform.SetParent(t_weaponSlot,false);
    }

    /// <summary>
    ///  기존무기파괴
    /// </summary>
    /// <param name="weapon"></param>
    void UnEquip(WeaponSlot weaponSlot)
    {
        TestWeapon currWeapon = equippedWeapon[weaponSlot];
        if (currWeapon)
        {
            Destroy(currWeapon.gameObject);
        }
    }


    /// <summary>
    /// 해당 무기를 파지한다. 
    /// </summary>
    /// <param name="weaponSlot"></param>
    void Hold(WeaponSlot weaponSlot)
    {
        TestWeapon weapon = equippedWeapon[weaponSlot];
        holdingWeapon = weapon;
        holdingSlot = weaponSlot;

        holding = true;
        animator.SetBool(hash_holding, holding);            // 무기를 장착하면 애니메이션 파라미터도 변경시킴   
        
        animator.Play($"Hold_{holdingWeapon.type}");

        Debug.Log($"무기장착 {weapon.gameObject.name}_{holdingWeapon.type}");

    }


    /// <summary>
    ///  해당 무기를 보관한다.
    /// </summary>
    void Holster()
    {
        holding = !holding;
        animator.SetBool(hash_holding, holding);        
    }

    /// <summary>
    /// 무장 해제한다. 
    /// </summary>
    void UnArm()
    {
        animator.Play("Unarmed");        
    }

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
