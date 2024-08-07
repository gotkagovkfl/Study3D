using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Study3D;

public class ItemObject_02_TestMeleeWeapon : Weapon
{
    public override string itemId =>  $"{(int)ItemType.Equipment}{(int)Study3D.EquipmentSlot.MeleeWeapon}";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
