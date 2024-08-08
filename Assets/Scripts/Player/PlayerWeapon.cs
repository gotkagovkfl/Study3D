using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// public enum WeaponCategory
// {
//     None,
//     Main,
//     Secondary,
//     Melee,
//     Support
// }
namespace Study3D
{
public class PlayerWeapon : MonoBehaviour
{
    PlayerEquipment playerEquipment;
    
    [SerializeField] Transform t_hand;           // 무기를 장착하는 손.
    
    EquipmentSlot currWeaponSlot ;
    Weapon holdingWeapon;     // 현재 사용중인 무기 

    
    //==============================================================

    // Start is called before the first frame update
    IEnumerator Start()
    {
        playerEquipment = GetComponent<PlayerEquipment>();
        
        yield return null;  // 다른 리소스 우선 초기화 되어야함. 
        
        InitCurrWeapons();                  // 시작시 보유중인 무기 초기화.
        HoldWeapon(EquipmentSlot.MainWeapon);    // 시작시 주무기 휴대
        Debug.Log(currWeaponSlot);
    }

    //=======================================================================

    /// 게임 시작시, 보유중인 무기 목록을 설정한다.  <= 나중에 로비에서 게임 시작 전 장착한 무기로 초기화할 예정. 
    void InitCurrWeapons()
    {

    }

    
    /// 보유중인 무기를 선택하여 사용 가능한 상태로 휴대한다. (swap)
    public void HoldWeapon(EquipmentSlot weaponSlot)
    {
        // 현재 들고있는 무기라면 무반응 
        if (currWeaponSlot == weaponSlot ) 
        {
            Debug.Log("이미 들고 있는 무기입니다");
            return;
        }


        // 
        currWeaponSlot = weaponSlot;

        // 이전무기 집어넣기
        holdingWeapon?.Holster();      

        // 선택한 무기 꺼내기
        holdingWeapon = playerEquipment.currEquipmentObjects[currWeaponSlot].GetComponent<Weapon>();
        holdingWeapon?.Hold();      
        //
    }

    

        


}

}
