using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponCategory
{
    None,
    Main,
    Secondary,
    Melee,
    Support
}


public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Transform t_hand;           // 무기를 장착하는 손.
    
    WeaponCategory currWeaponCategory;
    // EquippedWeapon holdingWeapon;     // <= 타입을 Weapon 타입으로 변경할 예정

    // Dictionary<WeaponCategory, EquippedWeapon> equippedWeapons = new();     // <= 나중에 Value 를 Weapon 타입으로 변경할 예정





    
    //==============================================================

    // Start is called before the first frame update
    void Start()
    {
        InitCurrWeapons();                  // 시작시 보유중인 무기 초기화.
        HoldWeapon(WeaponCategory.Main);    // 시작시 주무기 휴대
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //=======================================================================

    /// 게임 시작시, 보유중인 무기 목록을 설정한다.  <= 나중에 로비에서 게임 시작 전 장착한 무기로 초기화할 예정. 
    void InitCurrWeapons()
    {
        // equippedWeapons = new()
        // {
        //     {WeaponCategory.Main, null },
        //     {WeaponCategory.Secondary, null },
        //     {WeaponCategory.Melee, null },
        //     {WeaponCategory.Support, null },
        // };

        // EquipWeapon(  weapon_main_test );
        // EquipWeapon(  weapon_secondary_test );
        // EquipWeapon(  weapon_melee_test );
        // EquipWeapon(  weapon_support_test );
    }



    /// 무기 '소지' - 사용할때 바로 꺼낼 수 있도록 준비함.  < = 나중에 매개변수로 카테고리 삭제할거임. 매개변수에서 참조
    // void EquipWeapon(EquippedWeapon weapon)
    // {
        
        // Destroy( equippedWeapons[weaponCategory] );      //  <- 나중엔 파괴 여기서 말고.
        // GameObject newWeapon = Instantiate(weapon, t_hand);
        // newWeapon.transform.localPosition = Vector3.zero;

        // equippedWeapons[weaponCategory] = newWeapon; 
        

        // // 장착중인 카테고리 무기가 변경된거면,
        // if( currWeaponCategory == weaponCategory)
        // {
        //     holdingWeapon = newWeapon;       //사용처리 (수정해야함. )
        // }
        // else
        // {
        //     newWeapon.SetActive(false);        //안보이게 가리기 (수정해야함);
        // }
    // }

    
    /// 보유중인 무기를 선택하여 사용 가능한 상태로 휴대한다. (swap)
    public void HoldWeapon(WeaponCategory category)
    {
        // 현재 들고있는 무기라면 무반응 
        if (currWeaponCategory == category ) 
        {
            return;
        }


        // 
        currWeaponCategory = category;

        // 이전무기 집어넣기
        // holdingWeapon?.UnEquip();       // <= 수정해야함.

        // 선택한 무기 꺼내기
        // holdingWeapon = equippedWeapons[currWeaponCategory];
        // holdingWeapon.Equip();        // <= 수정해야함.

        //
    }

    

        


}