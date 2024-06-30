using UnityEngine;



// 플레이어 캐릭터를 조작하기 위한 사용자 입력을 감지
// 감지된 입력값을 다른 컴포넌트들이 사용할 수 있도록 제공

namespace Study3D
{
public class PlayerInput : MonoBehaviour 
{
    // 방향키       - wasd
    // 구르기       - space
    // 무기변경     - 1,2,3,4 
    // 공격         - 좌클
    // 보조공격     - 우클
    // 장전         - r 
    // 카메라 확대/축소 - 휠

    //---------------------------------------------------------------------------------
    readonly string verticalAxisName = "Vertical"; // 앞뒤 움직임을 위한 입력축 이름
    readonly string horizontalAxisName = "Horizontal"; // 좌우 회전을 위한 입력축 이름
    readonly string mainAttackButtonName = "Fire1"; // 발사를 위한 입력 버튼 이름
    readonly string secondaryAttackButtonName  = "Fire2";
    readonly string reloadButtonName = "Reload"; // 재장전을 위한 입력 버튼 이름

    KeyCode keyCode_dash = KeyCode.Space;
    KeyCode keyCode_jump = KeyCode.G;

    KeyCode keyCode_weapon_main = KeyCode.Alpha1;
    KeyCode keyCode_weapon_secondary = KeyCode.Alpha2;
    KeyCode keyCode_weapon_melee = KeyCode.Alpha3;
    KeyCode keyCode_weapon_support = KeyCode.Alpha4;

    //-----------------------------------------------------------------
    public Vector3 moveVector {get; private set;}   // 현재 움직임 벡터
    public Vector3 mouseScreenPos {get; private set;}       // 현재 마우스의 스크린상 위치
    public bool mainAttack { get; private set; } // 감지된 발사 입력값
    public bool secondaryAttack { get; private set; } // 감지된 발사 입력값
    public bool reload { get; private set; } // 감지된 재장전 입력값
    public bool dash { get; private set; }      // 감지된 대쉬 
    public bool jump { get; private set; }      // 감지된 점프 

    public bool weaponSelect_main { get; private set; }
    public bool weaponSelect_secondary { get; private set; }
    public bool weaponSelect_melee { get; private set; }
    public bool weaponSelect_support { get; private set; }
    

    //========================================================================================================

    // 매프레임 사용자 입력을 감지
    private void Update() 
    {
        // 게임오버 상태에서는 사용자 입력을 감지하지 않는다
        // if (GameManager.instance != null && GameManager.instance.isGameover)
        // {
        //     move = 0;
        //     rotate = 0;
        //     fire = false;
        //     reload = false;
        //     return;
        // }

        // move에 관한 입력 감지
        float move_v = Input.GetAxis(verticalAxisName);
        float move_h = Input.GetAxis(horizontalAxisName);
        moveVector =  new Vector3(move_h,0, move_v);

        mouseScreenPos = Input.mousePosition;           // 마우스 움직임
        
        //
        mainAttack = Input.GetButton(mainAttackButtonName);             // 주공격
        secondaryAttack = Input.GetButton(secondaryAttackButtonName);   // 보조공격

        reload = Input.GetButtonDown(reloadButtonName); // 장전

        dash = Input.GetKeyDown(keyCode_dash);      //회피
        jump = Input.GetKeyDown(keyCode_jump);      //점프
        
        weaponSelect_main = Input.GetKeyDown(keyCode_weapon_main);              // 주무기
        weaponSelect_secondary = Input.GetKeyDown(keyCode_weapon_secondary);    // 보조무기
        weaponSelect_melee = Input.GetKeyDown(keyCode_weapon_melee);            // 근접무기
        weaponSelect_support = Input.GetKeyDown(keyCode_weapon_support);        // 지원무기
    }
}
}

