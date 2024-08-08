
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ULT;
using System;

using DG.Tweening;

public enum WeaponSlot
{
    UnArmed,
    Primary,
    Secondary,
    Melee,
    Support
}



public class TestEquipment : MonoBehaviour
{
    [SerializeField] UltimatePlayerInput playerInput;
    [SerializeField] EquipmentSlot equipmentSlot;

    //
    Dictionary<WeaponSlot,TestWeapon> equippedWeapon = new(){ {WeaponSlot.Primary, null}, {WeaponSlot.Secondary, null}, {WeaponSlot.Melee, null} };
    [SerializeField]  WeaponSlot holdingSlot;
    [SerializeField]  TestWeapon holdingWeapon;


    public Animator animator;
    bool holding;
    readonly int hash_holding = Animator.StringToHash("Holding");

    [SerializeField] GameObject prefab_testRifle;
    [SerializeField] GameObject prefab_testPistol;


    Coroutine playingWork; // 중복된 작업이 아니라 마지막에 지정된 작업만 하기위함.



    //======================================================================================================================

    void Start()
    {
        playerInput = GetComponent<UltimatePlayerInput>();
        equipmentSlot = GetComponent<EquipmentSlot>();
    

        // ------- 기본 무기 장착 ------------
        TestWeapon w1= Instantiate(prefab_testRifle).GetComponent<TestWeapon>();
        Equip(WeaponSlot.Primary, w1);
        
        TestWeapon w2= Instantiate(prefab_testPistol).GetComponent<TestWeapon>();
        Equip(WeaponSlot.Secondary, w2);

    
        // 처음엔 무장 X - 나중엔 
        // Hold(WeaponSlot.Primary);

        StartCoroutine(SwitchWeapon(WeaponSlot.Primary));
    }

    void Update()
    {
        if (playerInput.weaponSelect_main)
        {
            StartWork(SwitchWeapon(WeaponSlot.Primary));
        }
        else if (playerInput.weaponSelect_secondary)
        {
            StartWork(SwitchWeapon(WeaponSlot.Secondary));
        }

        // 
        else if (Input.GetKeyDown(KeyCode.X))
        {
            StartWork(ToggleHolding());
        }

        // Debug.Log(handIK.weight);
    }


    //=======================================================================================


    //----------------------------

    /// <summary>
    /// weaponSlot 에 weapon을 장착한다. 
    /// </summary>
    /// <param name="weaponSlot"></param>
    /// <param name="weapon"></param>
    void Equip(WeaponSlot weaponSlot, TestWeapon weapon)
    {
        // 기존 무기 파괴
        UnEquip(weaponSlot);

        // 새 무기 장착
        equippedWeapon[weaponSlot] = weapon;
        Transform t_weaponSlot = equipmentSlot.weaponSlots[weapon.type];
        weapon.transform.SetParent(t_weaponSlot,false);
    }

    /// <summary>
    ///  기존무기파괴
    /// </summary>
    /// <param name="weapon"></param>
    void UnEquip(WeaponSlot weaponSlot)
    {
        TestWeapon currWeapon = equippedWeapon[weaponSlot];
        if (currWeapon)
        {
            Destroy(currWeapon.gameObject);
        }
    }

    //========================================== ==================================================
    // 무기 
    //==========================================

        /// <summary>
    /// 코루틴을 실행하되, 동시에 하나의 작업을 할 수 있도록 하기. - 진행중이던 작업은 즉시 멈춘다. 
    /// </summary>
    /// <param name="work"></param>
    void StartWork(IEnumerator work)
    {
        if (playingWork!=null)
            StopCoroutine(playingWork);
        
        playingWork = StartCoroutine(work);
    }

    //--------------------

    IEnumerator SwitchWeapon(WeaponSlot weaponSlot) 
    {
        if(holdingSlot == weaponSlot && holding)
        {
            Debug.Log("이미 들고있는 무기");
            yield break;
        }

        //들고있는 무기 holster
        // yield return StartCoroutine(HolsterWeaponC());
        yield return StartCoroutine(HolsterWeaponC());
        
        // 후에, 해당 슬롯 hold
        Hold(weaponSlot);
    }

    IEnumerator HolsterWeaponC()
    {
        if (holdingWeapon)
        {
            SetHolding(false);  
            // 홀스터 애니메이션을 기다린다. 
            do
            {
                yield return new WaitForEndOfFrame();
            }
            while( animator.GetCurrentAnimatorStateInfo(0).normalizedTime <1f);     // 스테이트 전환 시간이 설정되어있는 경우 잘 작동하지 않음. 
            
        }
    }

    IEnumerator ToggleHolding()
    {
        while( animator.GetCurrentAnimatorStateInfo(0).normalizedTime <1f)
        {
            yield return new WaitForEndOfFrame();
        }; 
        SetHolding(!holding);
    }

    
    //--------------------------------

    /// <summary>
    /// 해당 무기를 파지한다. 
    /// </summary>
    /// <param name="weaponSlot"></param>
    void Hold(WeaponSlot weaponSlot)
    {
          
        TestWeapon weapon = equippedWeapon[weaponSlot];
        holdingWeapon = weapon;
        holdingSlot = weaponSlot;

        Debug.Log($"무기장착 {weapon.gameObject.name}_{holdingWeapon.type}");

        SetHolding(true);
        animator.Play($"Hold_{holdingWeapon.type}");
    }


    /// <summary>
    ///  파지 상태를 지정한다. 
    /// </summary>
    void SetHolding(bool flag)
    {
        holding = flag;
        animator.SetBool(hash_holding, holding);        
    }

    //===============================================================

    /// <summary>
    /// 각 무기별로 파지 위치와 무기의 위치를 기억하기 위함. 
    /// </summary>
    // [ContextMenu("Save Weapon Pos")]
    // void SaveWeaponPos()
    // {
    //     GameObjectRecorder recorder = new(gameObject);
    //     recorder.BindComponentsOfType<Transform>(weaponParent.gameObject,false);
    //     recorder.BindComponentsOfType<Transform>(t_rHandGrip.gameObject,false);
    //     recorder.BindComponentsOfType<Transform>(t_lHandGrip.gameObject,false);
    //     recorder.TakeSnapshot(0f);
    //     recorder.SaveToClip(currWeapon.waeponAnimation);        // 현재 장착중인 무기 스크립트에서 무기의 애니메이션 참조를 받아 해당 파일에 저장.
    // }
}
