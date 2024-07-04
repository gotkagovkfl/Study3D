using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Study3D
{
public class TPSController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] float sensitivity_onNormal;
    [SerializeField] float sensitivity_onAiming;


    // private const float _threshold = 0.01f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // void LateUpdate()
    // {
    //     CameraRotation();

    //     // 마우스 입력을 받아들임
    //     float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    //     float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    //     // x축 회전값 계산 (위아래 움직임)
    //     xRotation -= mouseY;
    //     xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    //     // 카메라 회전 적용
    //     transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    //     // playerBody.Rotate(Vector3.up * mouseX);
    // }


    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;


    // void Update()
    // {
    //     // 마우스 입력을 받아들임
    //     float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    //     float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    //     // x축 회전값 계산 (위아래 움직임)
    //     xRotation -= mouseY;
    //     xRotation = Mathf.Clamp(xRotation, -90f, 90f);  

    //     // 카메라 회전 적용
    //     // Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    //     playerBody.Rotate(Vector3.up * mouseX);


    //     // Quaternion targetRotation = Quaternion.LookRotation(direction);
    //     // playerRb.rotation = targetRotation;
    // }



		// public void LookInput(Vector2 newLookDirection) 
		// {
		// 	look = newLookDirection;
		// }

		// private void CameraRotation()
		// {
		// 	// if there is an input and camera position is not fixed
		// 	if (look.sqrMagnitude >= _threshold && !LockCameraPosition)
		// 	{
		// 		_cinemachineTargetYaw += look.x * Time.deltaTime * sensitivity_onNormal;
        //         _cinemachineTargetPitch += look.y * Time.deltaTime * sensitivity_onNormal;
        //     }

		// 	// clamp our rotations so our values are limited 360 degrees
		// 	_cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
		// 	_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

		// 	// Cinemachine will follow this target
		// 	CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
		// }


		// private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		// {
		// 	if (lfAngle < -360f) lfAngle += 360f;
		// 	if (lfAngle > 360f) lfAngle -= 360f;
		// 	return Mathf.Clamp(lfAngle, lfMin, lfMax);
		// }




    public void Aim()
    {
        aimVirtualCamera.gameObject.SetActive(true);
        
    }

    public void UnAim()
    {
        aimVirtualCamera.gameObject.SetActive(false);
    }

}

}
