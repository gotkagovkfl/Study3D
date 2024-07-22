using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class VCamController : MonoBehaviour
{
    // CinemachineVirtualCamera vCam;
    [SerializeField] CinemachineVirtualCamera followCam;
    [SerializeField] CinemachineVirtualCamera AimCam;


    void Awake()
    {
        // vCam = GetComponent<CinemachineVirtualCamera>();
        // vCam.m_Transitions.m_OnCameraLive.AddListener(OnCameraLive);

        
    }
    void Start()
    {
        GameEvents.onPlayerAim.AddListener(OnPlayerAim);
        Camera.main.GetComponent<CinemachineBrain>().m_CameraActivatedEvent.AddListener(OnCameraActive); 
    }


    void OnPlayerAim(bool isOn)
    {
        if (isOn)
        {
            if (!AimCam.isActiveAndEnabled)
            {
                AimCam.gameObject.SetActive(true);
                followCam.gameObject.SetActive(false);
            }
        }
        else
        {
            if (!followCam.isActiveAndEnabled)
            {
                AimCam.gameObject.SetActive(false);
                followCam.gameObject.SetActive(true);
            }
        }
        // {
        //     if(!AimCam.activeSelf)    
        //         AimCam.SetActive(true);
        // }
        // else
        // {
        //     if(AimCam.activeSelf)   
        //         AimCam.SetActive(false);
        // }
    }


    /// <summary>
    /// newCam의 AxisState의 값을 oldCam 과 동기화 시킴. 
    /// 각 카메라의 AxisState는 활성 상태에서만 수정되고 동시에 한 카메라만 활성화 시킬 수 있기 때문에, 항상 다른 AxisState를 갖게 됨.
    /// 그래서 동기화를 직접 해줘야 카메라 변환에도 시점이 유지될 수 있음. - 만약 AxisState가 아니라, 입력에 따라 캐릭터를 직접 회전시키면 이럴 필요 없음. 
    /// </summary>
    /// <param name="newCam"></param>
    /// <param name="oldCam"></param>
    void OnCameraActive(ICinemachineCamera newCam, ICinemachineCamera oldCam)
    {
        Debug.Log("카메라 변환");
        // 처음 시작시, oldCam이 null임. 
        if (oldCam !=null)
        {   
            var newPov = newCam.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>();
            var currPov = oldCam.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>();

            newPov.m_VerticalAxis.Value = currPov.m_VerticalAxis.Value;
            newPov.m_HorizontalAxis.Value = currPov.m_HorizontalAxis.Value;


            // Debug.Log($"{oldCam.VirtualCameraGameObject)} : {currPov.m_HorizontalAxis.Value}, {currPov.m_VerticalAxis.Value}");
            // Debug.Log($"{newCam.VirtualCameraGameObject} :  {newPov.m_HorizontalAxis.Value}, {newPov.m_VerticalAxis.Value}");
        }
    }
}
