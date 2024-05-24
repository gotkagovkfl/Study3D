using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도


    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRb; // 플레이어 캐릭터의 리지드바디


        // private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터
    
    float dashSpeed = 10f;

    float lastUseTime_dash = -500;
    float cooltime_dash = 1f; 

    bool isAvailable_dash  => lastUseTime_dash + cooltime_dash  <= Time.time;


    //=======================================================================================================

    private void Start() 
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRb = GetComponent<Rigidbody>();
        // playerAnimator = GetComponent<Animator>();
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate() 
    {

        Rotate();
        Move();

        if (playerInput.dash && isAvailable_dash)
        {
            lastUseTime_dash = Time.time;
            Dash();
        }

            

        // playerAnimator.SetFloat("Move",playerInput.move);
    }

    //===================================================================================================================================

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move() 
    {
        // 이동 거리 계산 - 키입력이 없으면 0
        // Vector3 moveDistance = playerInput.moveVector* moveSpeed * Time.fixedDeltaTime;
        
        // 물리적용한 이동 
        // playerRb.MovePosition(playerRb.position + moveDistance);

        Vector3 moveDistance = playerInput.moveVector* moveSpeed ;
        
        // 물리적용한 이동 
        playerRb.velocity = moveDistance;

        // Debug.Log(playerRb.velocity);
        
    }

    void OnCollisionEnter(Collision collision)
    {
        // playerRb.velocity = Vector3.zero;

        
    }



    // 마우스 방향에 따라 캐릭터를 좌우로 회전
    private void Rotate() 
    {
        // Ray ray = Camera.main.ScreenPointToRay(playerInput.mouseScreenPos);
        
        // if(Physics.Raycast(ray,out RaycastHit hit))
        // {
        //     Vector3 mouseWorldPos = hit.point;       // 타겟을 레이캐스트가 충돌된 곳으로 옮긴다.
        //     mouseWorldPos.y = transform.position.y;

        //     Vector3  dir= mouseWorldPos - transform.position;

        //     Quaternion q = Quaternion.LookRotation(dir);
        //     playerRb.rotation  = q;
        // }


        Vector3 mouseScreenPos = playerInput.mouseScreenPos;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z; // 카메라와의 거리 설정

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.y = transform.position.y; // Y축 고정

        Vector3 direction = mouseWorldPos - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        playerRb.rotation = targetRotation;
    }

    void Dash()
    {
        // 물리적용한 이동 
        playerRb.velocity *= dashSpeed;
        Debug.Log(playerRb.velocity);

    }





}
