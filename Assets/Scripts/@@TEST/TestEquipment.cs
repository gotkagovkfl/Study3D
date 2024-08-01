using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TestEquipment : MonoBehaviour
{
    
    TestWeapon currWeapon;
    [SerializeField] Rig handIK;
    [SerializeField] Transform weaponPivot;


    void Start()
    {
        var weapon = GetComponentInChildren<TestWeapon>();
        if (weapon)
        {
            Equip(weapon);
        }
    }

    void Update()
    {
        if (currWeapon)
        {
                
        }
        else
        {

            handIK.weight= 0f;
        }
    }


    void Equip(TestWeapon weapon)
    {
        currWeapon = weapon;
        handIK.weight = 1f;

    }
}
