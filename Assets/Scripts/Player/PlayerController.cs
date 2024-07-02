using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    Study3D.PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트 - PlayerInput 클래스가 여러개 있어서.
    PlayerEquipment playerEquipment;
    PlayerWeapon playerWeapon;
    Rigidbody playerRb; // 플레이어 캐릭터의 리지드바디
    CapsuleCollider playerCollider;

    Animator playerAnimator;


        // private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    //-------------------------------
    
    // 이동
    float moveSpeed = 5f; // 앞뒤 움직임의 속도
    
    // 회전
    // float rotateSpeed = 180f; // 좌우 회전 속도

    // 점프
    float jumpSpeed = 12;       // 중력 2.5배로 설정해놨음. 
    bool isAvailable_jump 
    {
        get
        {
            
            if (Physics.Raycast(transform.position+ Vector3.up*0.1f, Vector3.down, out RaycastHit hit, playerCollider.height*0.5f))
            {
                return true;
            }
            return false;
        }
    }


    // 대시
    bool isDashing; 
    float duration_dash = 0.2f;
    float dashSpeed = 3f;

    float lastUseTime_dash = -500;
    float cooltime_dash = 1f; 

    bool isAvailable_dash  => lastUseTime_dash + cooltime_dash  <= Time.time;

    
    // 애니메이션
    List<Vector2> dirs = new()  // 8방향이 담겨있음. 위 방향을 기준으로 시계방향.
    {
        new Vector2(0,1),
        new Vector2(1,1),
        new Vector2(1,0),
        new Vector2(1,-1),
        new Vector2(0,-1),
        new Vector2(-1,-1),
        new Vector2(-1,0),
        new Vector2(-1,1)
    };

    Dictionary<Vector2,int> dirIdxs = new() // 해당 이동방향이 dir 리스트에서 몇번 idx인지,
    {
        {new Vector2(0,1),0},
        {new Vector2(1,1),1},
        {new Vector2(1,0),2},
        {new Vector2(1,-1),3},
        {new Vector2(0,-1),4},
        {new Vector2(-1,-1),5},
        {new Vector2(-1,0),6},
        {new Vector2(-1,1),7}
    };

    int idx_offset = 0;




    //=======================================================================================================

    private void Start() 
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<Study3D.PlayerInput>();
        playerEquipment = GetComponent<PlayerEquipment>();
        playerWeapon = GetComponent<PlayerWeapon>();
        playerRb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();

        playerAnimator = GetComponentInChildren<Animator>();
        
        // playerAnimator = GetComponent<Animator>();
    }

    /// LateUpdate : Fixed에 하니 입력이 씹힘. 
    void LateUpdate()
    {
        Rotate();
        Move();
        

        if (isAvailable_jump )
        {
            Debug.DrawRay(transform.position,  Vector3.down*playerCollider.height, Color.green,0,false);
        }
        else
        {
            Debug.DrawRay(transform.position,  Vector3.down*playerCollider.height, Color.red,0,false);
        }
    
        
        // 점프
        if (playerInput.jump && isAvailable_jump)
        {
            Jump();
            
        }
        // 대시
        if (playerInput.dash && isAvailable_dash)
        {
            lastUseTime_dash = Time.time;
            StartCoroutine(Dash());
        }

        
        // 무기변경
        if (playerInput.weaponSelect_main)
        {
            Debug.Log("[key] 주 무기 장착");
            SelectWeapon(EquipmentSlot.MainWeapon);
        }
        else if (playerInput.weaponSelect_secondary)
        {
            Debug.Log("[key] 보조 무기 장착");
            SelectWeapon(EquipmentSlot.SecondaryWeapon);
        }
        else if (playerInput.weaponSelect_melee)
        {
            Debug.Log("[key]근접 무기 장착");
            SelectWeapon(EquipmentSlot.MeleeWeapon);
        }
        else if (playerInput.weaponSelect_support)
        {
            Debug.Log("[key] 지원 무기 장착");
            SelectWeapon(EquipmentSlot.SupportWeapon);
        }
    }


    // void OnCollisionEnter(Collision collision)
    // {
        
    //     // 바닥인 경우
    //     // if (collision.gameObject.CompareTag("Platform"))
    //     // {
    //         if (collision.contacts[0].normal.y > 0f)
    //         {
    //             jumpCount = 0;
    //         }
    //     // }
    // }


    //===================================================================================================================================
    // 이동 애니메이션 설정
    void SetMoveAnimation(bool isMoving)
    {
        playerAnimator.SetBool("IsMoving",isMoving);    // idle <-> move
        if (isMoving)
        {
            // 현재 바라보는 방향 계산 (8방향)
            float rotY = playerRb.rotation.eulerAngles.y;
            // Debug.Log($"[플레이어 rot] {rotY}");

            if (22.5f <= rotY && rotY < 67.5f)
            {
                idx_offset = 1;
            }
            else if (67.5f <= rotY && rotY < 112.5f)
            {
                idx_offset = 2;
            }
            else if (112.5f <= rotY && rotY < 157.5f)
            {
                idx_offset = 3;
            }
            else if (157.5f<= rotY && rotY <202.5f)
            {
                idx_offset = 4;
            }
            else if (202.5f <= rotY && rotY < 247.5f)
            {
                idx_offset = 5;
            }
            else if (247.5f <=rotY && rotY <292.5f)
            {
                idx_offset = 6;
            }
            else if (292.5f <= rotY && rotY < 337.5f)
            {
                idx_offset = 7;
            }
            else 
            {
                idx_offset = 0;
            }
            // Debug.Log($"[회전방향 : ] {idx_offset}, {dirs[idx_offset]}"); 
            
            // 현재 이동방향 계산
            Vector2 moveVector = Vector3.zero;

            float moveH = playerInput.moveVector.x;
            float moveV = playerInput.moveVector.z;
            if (moveH !=0)
            {
                moveH = moveH>0?1:-1;
                moveVector.x = moveH;
            }
            if (moveV !=0)
            {
                moveV = moveV>0?1:-1;
                moveVector.y = moveV;
            }

            // 현재 바라보는 방향에서 현재 이동 방향으로 이동할 때 재생되어야 할 애니메이션 결정
            Vector2 animationVector = dirs[ (dirIdxs[moveVector] - idx_offset+8)%8];

            playerAnimator.SetFloat("MoveV",animationVector.x);
            playerAnimator.SetFloat("MoveV",animationVector.y);
        }
        
        
    }


    /// 입력값을 보고 해당 방향으로 이동. - velocity 안쓰면 벽뚫음.
    private void Move() 
    {
        // 대시 중에는 진행하지 않음 - velocity가 수정되기 떄문. 
        if (isDashing)
            return;
        
        //        
        Vector3 moveDistance =    playerInput.moveVector * moveSpeed ;

        playerRb.velocity = new Vector3(0,playerRb.velocity.y, 0) + moveDistance; // 낙하 등 중력은 보존함. 

        // 애니메이션 달리기 속도. 
        float hm = moveDistance.sqrMagnitude;
        SetMoveAnimation(hm>0);
    }

     /// 마우스 커서 방향으로 회전 
    private void Rotate() 
    {
        Vector3 mouseScreenPos = playerInput.mouseScreenPos;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z; // 카메라와의 거리 설정

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.y = transform.position.y; // Y축 고정

        Vector3 direction = mouseWorldPos - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        playerRb.rotation = targetRotation;
    }

    /// 점프
    void Jump()
    {
        // jumpCount++;
        playerRb.velocity += Vector3.up * jumpSpeed;
    }



    /// 대시 - 방향은 move로 인해 결정됨. 
    IEnumerator Dash()
    {
        // 물리적용한 이동 
        isDashing = true;

        Vector3 moveDistance = playerInput.moveVector.normalized* moveSpeed ;      // 여기서 재측정. 이속이 적게 측정됨. normalized 안하면 대시 거리가 달라짐.  
        playerRb.velocity = moveDistance * dashSpeed;
        Debug.Log($"{playerInput.moveVector} /      {moveDistance} /  {playerRb.velocity}" );

        yield return new WaitForSeconds(duration_dash);

        isDashing = false;
    }

    /// 무기교체
    void SelectWeapon(EquipmentSlot equipmentSlot)
    {
        playerWeapon.HoldWeapon(equipmentSlot);
    }

    





}
