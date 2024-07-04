using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



namespace  Study3D
{
    public interface IUsable
{
    public void Use();      // 인벤토리에서 우클릭했을 때, 
}



public enum ItemType
{
    Equipment   = 0,      // 장비
    Consumable  = 1,     // 소비
    Deployable  = 2,       // 설치 아이템 ( 블록 등 )
    Miscellaneous =3   // 기타 
}


//=======================================================================================
public abstract class Item
{
    public abstract string id{get;}     // 아이템 고유 id
    public abstract ItemType type {get;}        // 아이템 타입
}

//=======================================================================================


//
public abstract class Equipment :Item, IUsable     // 나중에 아이템 상속받을 예정
{
    // item 기본 정보
    
    public override ItemType type => ItemType.Equipment;

    // 장비 아이템의 정보    
    public abstract EquipmentSlot equipmentSlot {get;}      // 해당 장비 아이템의 장착 슬롯 위치
    


    //
    public void Use()
    {
        Equip();
    }

    public void Equip()
    {

    }

    public void UnEquip()
    {        

    }
}

//=======================================================================================

/// equipment_weapon_main_
public class Equipment_TestMainWeapon : Equipment
{
    public override string id => $"{(int)type}{(int)equipmentSlot}";
    public override EquipmentSlot equipmentSlot => EquipmentSlot.MainWeapon;
}


public class Equipment_TestSecondaryWeapon : Equipment
{
    public override string id => $"{(int)type}{(int)equipmentSlot}";
    public override EquipmentSlot equipmentSlot => EquipmentSlot.SecondaryWeapon;
}

public class Equipment_TestMeleeWeapon : Equipment
{
    public override string id => $"{(int)type}{(int)equipmentSlot}";
    public override EquipmentSlot equipmentSlot => EquipmentSlot.MeleeWeapon;
}

public class Equipment_TestSupportWeapon : Equipment
{
    public override string id => $"{(int)type}{(int)equipmentSlot}";
    public override EquipmentSlot equipmentSlot => EquipmentSlot.SupportWeapon;
}
}

