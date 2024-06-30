using System.Collections;
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
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, playerCollider.height * 0.5f + 0.1f))
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

        if (playerInput.jump && isAvailable_jump)
        {
            Jump();
        }
 
        if (playerInput.dash && isAvailable_dash)
        {
            lastUseTime_dash = Time.time;
            StartCoroutine(Dash());
        }

        
        //
        if (playerInput.weaponSelect_main)
        {
            Debug.Log("주 무기 장착");
            SelectWeapon(EquipmentSlot.MainWeapon);
        }
        else if (playerInput.weaponSelect_secondary)
        {
            Debug.Log("보조 무기 장착");
            SelectWeapon(EquipmentSlot.SecondaryWeapon);
        }
        else if (playerInput.weaponSelect_melee)
        {
            Debug.Log("근접 무기 장착");
            SelectWeapon(EquipmentSlot.MeleeWeapon);
        }
        else if (playerInput.weaponSelect_support)
        {
            Debug.Log("지원 무기 장착");
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
        hm = hm>0 ? 1f :0;
        playerAnimator.SetFloat("MoveSpeed",hm);
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
