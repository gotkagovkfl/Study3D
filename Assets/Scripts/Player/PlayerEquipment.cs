using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 플레이어의 장비 장착 칸
public enum EquipmentSlot 
{
    MainWeapon,
    SecondaryWeapon,
    MeleeWeapon,
    SupportWeapon,

}


// 플레이어의 장비 관리자
public class PlayerEquipment : MonoBehaviour
{
    Dictionary<EquipmentSlot, Transform> equipmentPositions;    // 장비 착용 위치  - 아직 이거까진 투머치 
    
    Dictionary<EquipmentSlot, Equipment> currEquipments;   // 현재 장착중인 장비 목록

    


        ///for test 
    [Header("Test")]
    
    // 이것들은 프리팹
    [SerializeField] Equipment weapon_main_test;
    [SerializeField] Equipment weapon_secondary_test;
    [SerializeField] Equipment weapon_melee_test;
    [SerializeField] Equipment weapon_support_test;



    //=============================================================================================

    // 초기화
    public void Init()
    {
        //  일단 현재 장착 장비 사전 초기화. 
        
        equipmentPositions = new();
        foreach (EquipmentSlot equipmentSlot in Enum.GetValues(typeof(EquipmentSlot)))
        {
            equipmentPositions.Add( equipmentSlot, null);
        }




        currEquipments = new();
        foreach (EquipmentSlot equipmentSlot in Enum.GetValues(typeof(EquipmentSlot)))
        {
            currEquipments.Add( equipmentSlot, null);
        }

        // 그리고 플레이어의 저장된 정보를 보고 해당 장비 장착
        InitTestWeapon();
        
    }
    

    /// 테스트용으로 무기들을 초기화함. 
    void InitTestWeapon()
    {   
        // currEquipments[EquipmentSlot.MainWeapon] = null;
        // currEquipments[EquipmentSlot.SecondaryWeapon] = null;
        // currEquipments[EquipmentSlot.MeleeWeapon] = null;
        // currEquipments[EquipmentSlot.SupportWeapon] = null;
        
    }


    void Swap(Equipment equipment)
    {
        EquipmentSlot currEquipmentSlot= equipment.equipmentSlot;

        
        // 사용중이던 장비 장착해제
        currEquipments[currEquipmentSlot]?.UnEquip();

        // 해당 장비 장착.
        Transform t_equipmentPosition = equipmentPositions[currEquipmentSlot];

        currEquipments[currEquipmentSlot] = equipment;
        currEquipments[currEquipmentSlot]?.Equip( t_equipmentPosition );
    }


    void UnEquip(EquipmentSlot equipmentSlot)
    {
        // 사용중이던 장비 장착해제
        currEquipments[equipmentSlot]?.UnEquip();
        currEquipments[equipmentSlot] = null;
    }

}
