using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



// 플레이어의 장비 장착 칸
public enum EquipmentSlot 
{
    None = 0,
    MainWeapon = 1,
    SecondaryWeapon = 2,
    MeleeWeapon = 3,
    SupportWeapon = 4,

}


// 플레이어의 장비 관리자
public class PlayerEquipment : MonoBehaviour
{
    
    
    Dictionary<EquipmentSlot, Equipment> currEquipments;   // 현재 장착중인 장비 목록   

    public Dictionary<EquipmentSlot, Transform> equipmentPositions;    // 장비 착용 위치  - 아직 이거까진 투머치 
    public Dictionary<EquipmentSlot, GameObject> currEquipmentObjects; //현재 장착중인 장비의 게임 오브젝트 

    


    ///for test
    [Header("Test")]
    
    // 이것들은 프리팹
    [SerializeField] Equipment weapon_main_test;
    [SerializeField] Equipment weapon_secondary_test;
    [SerializeField] Equipment weapon_melee_test;
    [SerializeField] Equipment weapon_support_test;



    //=============================================================================================
    void Start()
    {
        Init();
    }


    // 초기화
    public void Init()
    {
        //  일단 현재 장착 장비 사전 초기화. 
        equipmentPositions = new();
        currEquipments = new();
        currEquipmentObjects = new();
        
        foreach (EquipmentSlot equipmentSlot in Enum.GetValues(typeof(EquipmentSlot)))
        {
            equipmentPositions.Add( equipmentSlot, null);
            currEquipments.Add( equipmentSlot, null);
            currEquipmentObjects.Add( equipmentSlot, null);
        }

        InitEquipPos();
        // 그리고 플레이어의 저장된 정보를 보고 해당 장비 장착
        InitTestWeapon();
    }
    
    void InitEquipPos()
    {
        Transform t_temp = transform.Find("Temp");
        
        equipmentPositions[EquipmentSlot.MainWeapon] = t_temp;
        equipmentPositions[EquipmentSlot.SecondaryWeapon] = t_temp;
        equipmentPositions[EquipmentSlot.MeleeWeapon] = t_temp;
        equipmentPositions[EquipmentSlot.SupportWeapon] = t_temp;
    }


    /// 테스트용으로 무기들을 초기화함. 
    void InitTestWeapon()
    {   
        Equip(EquipmentSlot.MainWeapon,     new Equipment_TestMainWeapon() );         // 테스트 주무기
        Equip(EquipmentSlot.SecondaryWeapon,new Equipment_TestSecondaryWeapon());     // 테스트 보조무기
        Equip(EquipmentSlot.MeleeWeapon,    new Equipment_TestMeleeWeapon());         // 테스트 근접무기
        Equip(EquipmentSlot.SupportWeapon,  new Equipment_TestSupportWeapon());       // 테스트 지원무기

        Debug.Log("[ 테스트 무기 초기화 됨]");
    }



    /// <summary>
    /// 장착중인 장비를 교체할 때,
    /// </summary>
    /// <param name="equipment"></param>
    void Swap(Equipment equipment)
    {
        EquipmentSlot currEquipmentSlot= equipment.equipmentSlot;
        
        // 사용중이던 장비 장착해제
        UnEquip(currEquipmentSlot);

        // 새 장비 장착
        Equip(currEquipmentSlot, equipment);
    }


    void Equip(EquipmentSlot equipmentSlot, Equipment equipment)
    {
        // 해당 장비 장착
        currEquipments[equipmentSlot] = equipment;
        currEquipments[equipmentSlot]?.Equip( );

        // 실체 생성해야함. 
        string id = equipment.id;
        
        // 나중에 아이템 매니저든 뭐든으로 전용 클래스에서 생성하자. 지금은 굉장히 비효율 적인 작업을 하고 있다.
        GameObject eo = null;
        foreach(var o in Resources.LoadAll<GameObject>("Prefabs/Weapons"))
        {
            string itemObjectId = o.GetComponent<ItemObject>().itemId;
            // Debug.Log(o.name);
            if (itemObjectId == id)
            {
                Transform equipPos = equipmentPositions[equipmentSlot];
                eo = Instantiate(o,equipPos);
                eo.transform.localPosition = Vector3.zero;

                currEquipmentObjects[equipmentSlot] = eo;

                break;
            }
        }   
        
        Debug.Log($"[아이템 장착] {id} ,{eo?.name}");
    }


    void UnEquip(EquipmentSlot equipmentSlot)
    {
        // 사용중이던 장비 장착해제
        currEquipments[equipmentSlot]?.UnEquip();
        currEquipments[equipmentSlot] = null;

        // 만들어진 실체 없애기 
        Destroy(currEquipmentObjects[equipmentSlot]);
    }

}
