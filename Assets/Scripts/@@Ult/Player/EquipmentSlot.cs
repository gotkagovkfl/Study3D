using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    public Dictionary<WeaponType, Transform> weaponSlots;      // 무기를 장착할 위치
    


    
    // ----------------- 무기 --------------------
    public Transform weaponSlot_rifle;  // rifle
    public Transform weaponSlot_pistol; // pistol




    //===================================================================================
    void Awake()
    {
        // 이건 여기서 초기화해야함.- 초기화 방식이 달라져야한다. 나중엔, 현재 장착한 무기 타입별로 설정하자. 
        weaponSlots = new(){ {WeaponType.Rifle, weaponSlot_rifle}, {WeaponType.Pistol, weaponSlot_pistol}};
    }




}
