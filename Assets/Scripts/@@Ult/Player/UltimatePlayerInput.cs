using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ULT
{
    // 
    public class UltimatePlayerInput : MonoBehaviour
    {
        // 방향키       - wasd
        // 점프         - g
        // 구르기       - space
        // 무기변경     - 1,2,3,4 
        // 공격         - 좌클
        // 조준         - 우클
        // 장전         - r 
        // 카메라 확대/축소 - 휠

        //---------------------------------------------------------------------------------
        
        readonly string mouseHorizontalAxisName = "Mouse X"; // 좌우 회전을 위한 입력축 이름
        readonly string mouseVerticalAxisName = "Mouse Y"; // 앞뒤 움직임을 위한 입력축 이름
        
        readonly string verticalAxisName = "Vertical"; // 앞뒤 움직임을 위한 입력축 이름
        readonly string horizontalAxisName = "Horizontal"; // 좌우 회전을 위한 입력축 이름
        readonly string mainAttackButtonName = "Fire1"; // 발사를 위한 입력 버튼 이름
        readonly string secondaryAttackButtonName  = "Fire2";
        readonly string reloadButtonName = "Reload"; // 재장전을 위한 입력 버튼 이름
        readonly string AimButtonName = "Aim"; // 조준을 위한 입력 버튼 이름

        KeyCode keyCode_dash = KeyCode.Space;
        KeyCode keyCode_jump = KeyCode.G;

        KeyCode keyCode_weapon_main = KeyCode.Alpha1;
        KeyCode keyCode_weapon_secondary = KeyCode.Alpha2;
        KeyCode keyCode_weapon_melee = KeyCode.Alpha3;
        KeyCode keyCode_weapon_support = KeyCode.Alpha4;

        //-----------------------------------------------------------------
        // 마우스 움직임
        public float mouseMoveH {get;private set;}
        public float mouseMoveV {get;private set;}
        
        // 키보드 이동 
        public float move_h {get;private set;}
        public float move_v {get;private set;}
        public Vector3 moveVector {get;private set;}



        public Vector3 mouseScreenPos {get; private set;}       // 현재 마우스의 스크린상 위치
        public bool mainAttack { get; private set; } // 감지된 발사 입력값
        public bool secondaryAttack { get; private set; } // 감지된 발사 입력값
        public bool aim {get; private set;}
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

            // 마우스 움직임에 관한 입력 감지
            mouseMoveH = Input.GetAxisRaw(mouseHorizontalAxisName);
            mouseMoveV = Input.GetAxisRaw(mouseVerticalAxisName);

            // move에 관한 입력 감지
            move_v = Input.GetAxis(verticalAxisName);
            move_h = Input.GetAxis(horizontalAxisName);

            moveVector = new Vector3(move_h,0,move_v);

            mouseScreenPos = Input.mousePosition;           // 마우스 움직임
            
            //
            mainAttack = Input.GetButton(mainAttackButtonName);             // 주공격
            secondaryAttack = Input.GetButton(secondaryAttackButtonName);   // 보조공격
            aim = Input.GetButton(AimButtonName);

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

