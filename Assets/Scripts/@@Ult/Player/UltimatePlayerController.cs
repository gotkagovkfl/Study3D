using System.Runtime.CompilerServices;
using UnityEngine;
// using UnityEngine.InputSystem;

namespace ULT
{
    [RequireComponent(typeof(CharacterController))]
    public class UltimatePlayerController : MonoBehaviour
    {
        UltimatePlayerNewInput playerInput;
        [SerializeField] private CharacterController controller;
        
        //상태
        // public bool IsGrounded => controller.isGrounded;



        [SerializeField]
        private Vector3 playerVelocity;
        
        #region Move
        [SerializeField] 
        private Vector3 lastMoveDir;
        [SerializeField] 
        private float playerSpeed = 5f;
        #endregion

        #region Jump
        [SerializeField]
        private bool groundedPlayer;
        [SerializeField]
        private float jumpHeight = 1.0f;
        [SerializeField]
        private float gravityValue = -9.81f;
        #endregion

        #region Rotate      
        // [SerializeField]
        // Transform t_camera;
        [SerializeField]
        float rotationSpeed = 10f;
        #endregion

        #region Aim
        [SerializeField]
        bool isAiming;
        #endregion

        #region Shoot
        [SerializeField]
        GameObject prefab_bullet;
        [SerializeField]
        Transform t_muzzle;
        [SerializeField]
        Transform t_bulletParent;

        #endregion

        #region Animation
        Animator animator;
        [SerializeField]
        int animParaId_moveX, animParaId_moveZ;
        int jumpAnimation;
        [SerializeField]
        Vector2 currAnimBlendVector;
        [SerializeField]
        Vector2 animVelocity;
        [SerializeField]
        float animSmoothTime = 0.05f;
        [SerializeField]
        float animationTransition  = 0.1f;
        
        #endregion
    

        //====================================================================================

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            playerInput = GetComponent<UltimatePlayerNewInput>();
            
            // t_camera = Camera.main.transform;

            animator = GetComponent<Animator>();
            animParaId_moveX = Animator.StringToHash("MoveX");
            animParaId_moveZ = Animator.StringToHash("MoveZ");
            jumpAnimation = Animator.StringToHash("Jump");
        }

        void Update()
        {
            // gravity
            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            
            //
            groundedPlayer =  controller.isGrounded;        // controller.isGrounded는 해당 프로퍼티 호출 직전 움직임을 보고 접지 여부를 판단하기 때문에, 프레임 별 딱 한번 호출해야한다. 
            if (groundedPlayer  && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Debug.Log($"{groundedPlayer}  ");

            // jump
            if (groundedPlayer && playerInput.jump)
            {
                Jump();
            }

            Move();
            

            Rotate(playerInput.mouseWorldPos);


            //------------ 

            // aim
            Aim(playerInput.aim);

            //shoot
            if (playerInput.shoot)
            {
                Shoot(playerInput.mouseWorldPos);
            }


        }

        //============================================================================

        void Rotate(Vector3 targetPos)
        {
            // 조준 방향으로 몸 회전 
            targetPos.y = transform.position.y;    //y고정
            Vector3 dir_bodyToAim = (targetPos - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, dir_bodyToAim,Time.deltaTime * 20f);
            Debug.DrawRay(transform.position, dir_bodyToAim*10, Color.green,0,true);
        }

        void Move()
        {
            // 땅위의 경우
            Vector2 moveVector = playerInput.moveVector;
            lastMoveDir = transform.right* moveVector.x + transform.forward * moveVector.y;
            lastMoveDir.y = 0;      // 방향 조절에 필요 없기떄문.
            controller.Move(lastMoveDir.normalized * Time.deltaTime * playerSpeed);

            // animations
            currAnimBlendVector = Vector2.SmoothDamp(currAnimBlendVector, moveVector,ref animVelocity,animSmoothTime);  // 애니메이션 간 자연스러운 전환을 위해
            animator.SetFloat(animParaId_moveX, currAnimBlendVector.x);
            animator.SetFloat(animParaId_moveZ, currAnimBlendVector.y);
            // if (IsGrounded) // 발이 땅에 붙어있는 경우에만 이동 애니메이션 
            // {
                

            animator.SetBool("IsGrounded",groundedPlayer);
            
                // }
        }

        void Jump()
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

            animator.CrossFade(jumpAnimation,animationTransition);
            animator.SetBool("IsGrounded",false);
        }


        /// <summary>
        /// 조준. - 
        /// </summary>
        /// <param name="isOn"></param>
        void Aim(bool isOn)
        {
            isAiming = isOn;
            GameEvents.onPlayerAim.Invoke(isOn);
        }

        void Shoot(Vector3 targetPos)
        {
            Debug.Log("발사!");
        
            Vector3 dir_muzzleToAim = (targetPos - t_muzzle.position).normalized;
            Instantiate(prefab_bullet,t_muzzle.position, Quaternion.LookRotation(dir_muzzleToAim, Vector3.up));
        }

    }
    

}