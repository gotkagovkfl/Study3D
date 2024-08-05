using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    // public static GameEvents events;

    public static UnityEvent<bool> onPlayerAim;

    //
    // public static UnityEvent<int, bool> onWeaponAnimation;  // 무기 애니메이션  (int : 무기 슬롯 번호 , bool : t: isEquiping , f: isHolstering)
    
    void Awake()
    {
        onPlayerAim = new();
    }

}
