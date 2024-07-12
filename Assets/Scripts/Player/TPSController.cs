using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

namespace Study3D
{
    public class TPSController : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera aimVirtualCamera;

        [SerializeField] StarterAssetsInputs starterAssetsInputs;

        ThirdPersonController thirdPersonController;

        [SerializeField] float sensitivity_onNormal =1f;
        [SerializeField] float sensitivity_onAiming = 0.9f;

        [SerializeField] LayerMask aimColliderLayerMask = new();
        [SerializeField] Transform t_debug;
        [SerializeField] Transform t_muzzle;
        [SerializeField] Transform t_testProjectile;


        Animator animator;

        // private const float _threshold = 0.01f;


        void Awake()
        {
            starterAssetsInputs = GetComponent<StarterAssetsInputs>();
            thirdPersonController = GetComponent<ThirdPersonController>();

            animator = GetComponent<Animator>();
        }

        void Update()
        {
            Transform t_hit = null; // 히트스캔에 필요.
            // 조준점이 가리키는 화면 좌표구하기. 
            Vector3 mouseWorldPosition;
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width *0.5f, Screen.height * 0.5f) );  //조준점 위치(화면중앙));
            // 조준점 방향 계산
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
            {
                mouseWorldPosition =  raycastHit.point;
                

                t_hit = raycastHit.transform;
            }
            else
            {
                mouseWorldPosition = ray.GetPoint(50); // 적절한 거리로 설정
            }
            

            
            // 조준 상태에 따라 플레이어 처리
            Aim(starterAssetsInputs.aim, mouseWorldPosition);


            //발사
            if (starterAssetsInputs.attack)
            {
                Vector3 dir_muzzleToAim = (mouseWorldPosition - t_muzzle.position).normalized;

                Instantiate(t_testProjectile,t_muzzle.position, Quaternion.LookRotation(dir_muzzleToAim, Vector3.up));
            }

            // 디버그용
            t_debug.position = mouseWorldPosition;  // 조준점 충돌위치에 오브젝트 위치시키기
        }


        //
        void Aim(bool isOn,Vector3 mouseWorldPosition)
        {
            float weight =0;
            float sensitivity = sensitivity_onNormal ;
            if (isOn)
            {
                weight =1;
                sensitivity *= sensitivity_onAiming;

                
                // 조준 방향으로 몸 회전 
                Vector3 worldAimTarget = mouseWorldPosition;
                worldAimTarget.y = transform.position.y;    //잠깐 고정
                Vector3 dir_bodyToAim = (worldAimTarget - transform.position).normalized;

                transform.forward = Vector3.Lerp(transform.forward, dir_bodyToAim,Time.deltaTime * 20f);
                Debug.DrawRay(transform.position, dir_bodyToAim*10, Color.green,0,true);
            }
            aimVirtualCamera.gameObject.SetActive(isOn);
            thirdPersonController.SetRotateOnMove(!isOn);
            thirdPersonController.SetSensitivity(sensitivity);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), weight, Time.deltaTime  *10f));
        }
    }

}
