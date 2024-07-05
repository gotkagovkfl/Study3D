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


    // private const float _threshold = 0.01f;


    void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    void Update()
    {
        // 조준점이 가리키는 화면 좌표구하기. 
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width *0.5f, Screen.height * 0.5f);
        
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition =  raycastHit.point;
            t_debug.position = raycastHit.point;
        }
        
        
        // 조준 상태
        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(sensitivity_onAiming);
            thirdPersonController.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;    //잠깐 고정
            Vector3 aimDir = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDir,Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(sensitivity_onNormal);
            thirdPersonController.SetRotateOnMove(true);
        }


        if (starterAssetsInputs.attack)
        {
            Debug.Log("attack");
            Vector3 aimDir = (mouseWorldPosition - t_muzzle.position).normalized;
            Instantiate(t_testProjectile,t_muzzle.position, Quaternion.LookRotation(aimDir, Vector3.up));
            starterAssetsInputs.attack = false;
        }
    }


    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;



    public void Aim()
    {
        aimVirtualCamera.gameObject.SetActive(true);
        thirdPersonController.SetSensitivity(sensitivity_onAiming);
    }

    public void UnAim()
    {
        aimVirtualCamera.gameObject.SetActive(false);
        thirdPersonController.SetSensitivity(sensitivity_onNormal);
    }

}

}
