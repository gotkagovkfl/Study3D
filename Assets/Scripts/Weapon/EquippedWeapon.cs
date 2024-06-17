using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// 현재 장비창에 장착한 무기 ( 사용가능한 무기 )
public class EquippedWeapon : MonoBehaviour
{
    // 무기 정보
    Weapon weapon;  

    /// 손에 든다. (사용한다)
    public void Hold()
    {
        gameObject.SetActive(true);
    }


    public void Equip()
    {
        gameObject.SetActive(false);
    }


    /// 무기 장착해제
    public void UnEquip()
    {
        Destroy(gameObject);        // 오브젝트 파괴   
    }
}
