using System.Runtime.CompilerServices;
using UnityEngine;
// using UnityEngine.InputSystem;

namespace ULT
{
    [RequireComponent(typeof(CharacterController))]
    public class UltimatePlayerController : MonoBehaviour
    {
        UltimatePlayerNewInput playerInput;
        private CharacterController controller;
        [SerializeField]
        private Vector3 playerVelocity;
        
        #region Move
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
        [SerializeField]
        Transform t_camera;
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

    


        //====================================================================================

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            playerInput = GetComponent<UltimatePlayerNewInput>();

            t_camera = Camera.main.transform;
        }

        void Update()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            // jump
            if (playerInput.jump && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            // move
            Vector3 moveVector = playerInput.moveVector;
            Vector3 dir = t_camera.right.normalized * moveVector.x + t_camera.forward.normalized * moveVector.y;
            dir.y = 0;      // 방향 조절에 필요 없기떄문.
            controller.Move(dir.normalized * Time.deltaTime * playerSpeed);


            // rotate
            Rotate(playerInput.mouseWorldPos);


            // gravity
            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);


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