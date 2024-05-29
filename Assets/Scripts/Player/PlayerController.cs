using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도


    PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    PlayerWeapon playerWeapon;
    Rigidbody playerRb; // 플레이어 캐릭터의 리지드바디


        // private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터
    

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
        playerInput = GetComponent<PlayerInput>();
        playerWeapon = GetComponent<PlayerWeapon>();
        playerRb = GetComponent<Rigidbody>();
        // playerAnimator = GetComponent<Animator>();
    }

    /// LateUpdate : Fixed에 하니 입력이 씹힘. 
    void LateUpdate()
    {
        Rotate();
        Move();
 
        if (playerInput.dash && isAvailable_dash)
        {
            lastUseTime_dash = Time.time;
            StartCoroutine(Dash());
        }

        
        //
        if (playerInput.weaponSelect_main)
        {
            Debug.Log("주 무기 장착");
            SelectWeapon(WeaponCategory.Main);
        }
        else if (playerInput.weaponSelect_secondary)
        {
            Debug.Log("보조 무기 장착");
            SelectWeapon(WeaponCategory.Secondary);
        }
        else if (playerInput.weaponSelect_melee)
        {
            Debug.Log("근접 무기 장착");
            SelectWeapon(WeaponCategory.Melee);
        }
        else if (playerInput.weaponSelect_support)
        {
            Debug.Log("지원 무기 장착");
            SelectWeapon(WeaponCategory.Support);
        }
    }

    //===================================================================================================================================

    /// 입력값을 보고 해당 방향으로 이동. - velocity 안쓰면 벽뚫음.
    private void Move() 
    {
        // 대시 중에는 진행하지 않음 - velocity가 수정되기 떄문. 
        if (isDashing)
            return;
        
        //        
        playerRb.velocity = Vector3.zero;
        Vector3 moveDistance = playerInput.moveVector* moveSpeed ;
        playerRb.velocity = moveDistance;
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
    void SelectWeapon(WeaponCategory category)
    {
        playerWeapon.UseWeapon(category);
    }

    





}
