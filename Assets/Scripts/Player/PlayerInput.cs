using Unity.VisualScripting;
using UnityEngine;

// 플레이어 캐릭터를 조작하기 위한 사용자 입력을 감지
// 감지된 입력값을 다른 컴포넌트들이 사용할 수 있도록 제공
public class PlayerInput : MonoBehaviour 
{
    public string verticalAxisName = "Vertical"; // 앞뒤 움직임을 위한 입력축 이름
    public string horizontalAxisName = "Horizontal"; // 좌우 회전을 위한 입력축 이름
    // public string fireButtonName = "Fire1"; // 발사를 위한 입력 버튼 이름
    // public string reloadButtonName = "Reload"; // 재장전을 위한 입력 버튼 이름

    // 값 할당은 내부에서만 가능
    // public float move_v { get; private set; } // 감지된 움직임 입력값
    // public float move_h { get; private set; } // 감지된 회전 입력값
    // public bool fire { get; private set; } // 감지된 발사 입력값
    // public bool reload { get; private set; } // 감지된 재장전 입력값


    public Vector3 moveVector {get; private set;}   // 현재 움직임 벡터

    public Vector3 mouseScreenPos {get; private set;}       // 현재 마우스의 스크린상 위치

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

        mouseScreenPos = Input.mousePosition;


        // fire에 관한 입력 감지
        // fire = Input.GetButton(fireButtonName);


        // reload에 관한 입력 감지
        // reload = Input.GetButtonDown(reloadButtonName);
    }
}