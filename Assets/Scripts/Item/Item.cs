using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;





public interface IUsable
{
    public void Use();
}



public enum ItemType
{
    Equipment,      // 장비
    Consumable,     // 소비
    Deployable,       // 설치 아이템 ( 블록 등 )
    Miscellaneous   // 기타 
}


//
public class Item 
{
    public string data{get; protected set;}


    protected Sprite icon;          // ui에 표시되는 아이콘
    protected GameObject prefab_object;  //  화면에 그려지는 오브젝트 물체 프리팹- 이걸 어디선가 할당해야하는데, 
    protected GameObject realObject;    //실체화된 아이템. - 씬에 버러지거나, 장착했을때, 



    /// 아이템 실체화 - 오브젝트 생성하여 월드에 배치 
    // public void Materialize()
    // {
    //     realObject = GameManager.gm.Help_instantiate(prefab_object,Vector3.zero, Quaternion.identity); //일단 이부분은 나중에 고쳐보자.
    // }

    //     /// 아이템 실체화 - 오브젝트 생성하여 월드에 배치 
    // public void Materialize(Transform t_parent)
    // {
    //     realObject = GameManager.gm.Help_instantiate(prefab_object,t_parent);
    // }

    // /// 아이템 실체 제거 
    // public void DeMaterialize()
    // {
    //     GameManager.gm.Help_destroy(realObject);
    // }
}


//
public abstract class Equipment :Item     // 나중에 아이템 상속받을 예정
{
    public abstract EquipmentSlot equipmentSlot {get;}      // 해당 장비 아이템의 장착 슬롯 위치

    public void Equip(Transform t_equipmentPosition)
    {
        // if (t_equipmentPosition!=null)
        // {
        //     Materialize(t_equipmentPosition);          // 아이템 구체화 
        // }
            

        // 아이템의 능력치가 있다면, 능력치도 조정해줘야함. -- 나중에, 플레이어 스탯 만들고,

        // 그리고 사용가능한 장비면, 초기화 한번 해줘야함.(ex, weapon)
        
    }

    public void UnEquip()
    {        
        // 아이템의 능력치가 있다면, 능력치도 조정해줘야함. -- 나중에, 플레이어 스탯 만들고,

        // DeMaterialize();    // 구체화된 아이템 파괴
    }
}



/// equipment_weapon_main_
public class E_W_M_Test : Equipment
{
    public override EquipmentSlot equipmentSlot => EquipmentSlot.MainWeapon;


    public void Hold()
    {
        // Materialize()
        // {
            
        // }        
    }
}
