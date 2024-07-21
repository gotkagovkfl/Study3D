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
        [SerializeField]
        private bool groundedPlayer;
        [SerializeField] 
        private float playerSpeed = 2.0f;
        [SerializeField]
        private float jumpHeight = 1.0f;
        [SerializeField]
        private float gravityValue = -9.81f;




        private void Start()
        {
            controller = GetComponent<CharacterController>();
            playerInput = GetComponent<UltimatePlayerNewInput>();
        }

        void Update()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
            
            // 시점에 따라 다르게 변경해야함. 현재는 tps 기준. 
            Vector3 dir = transform.forward * playerInput.moveVector.x + transform.right * playerInput.moveVector.y;
            controller.Move(playerInput.moveVector * Time.deltaTime * playerSpeed);

            // 이부분은 회전.
            // if (dir != Vector3.zero)
            // {
            //     gameObject.transform.forward = move;
            // }

            // Changes the height position of the player..
            if (playerInput.jump && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}

