using System.Runtime.CompilerServices;
using UnityEngine;
// using UnityEngine.InputSystem;

namespace ULT
{
    [RequireComponent(typeof(CharacterController))]
    public class UltimatePlayerController : MonoBehaviour
    {
        // UltimatePlayerInput playerInput;

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
            Quaternion targetRot = Quaternion.Euler(0,t_camera.eulerAngles.y,0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);


            // gravity
            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            // aim
            Aim(playerInput.aim);

            if (playerInput.shoot)
            {
                Shoot();
            }
        }

        //============================================================================

        /// <summary>
        /// 조준. - 
        /// </summary>
        /// <param name="isOn"></param>
        void Aim(bool isOn)
        {
            isAiming = isOn;
            // if (isAiming)
            // {
            //     if(!AimCam.activeSelf)    
            //         AimCam.SetActive(true);
            // }
            // else
            // {
            //     if(AimCam.activeSelf)   
            //         AimCam.SetActive(false);
            // }
            GameEvents.onPlayerAim.Invoke(isOn);
        }

        void Shoot()
        {
            Debug.Log("발사!");
            Instantiate(prefab_bullet, t_muzzle.position, Quaternion.identity,t_bulletParent);
        }

    }
    

}