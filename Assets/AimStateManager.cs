using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using ULT;

public class AimStateManager : MonoBehaviour
{
    UltimatePlayerInput playerInput;
    
    
    
    public Animator animator;
    
    public float xAxis, yAxis;

    
    [SerializeField] Transform camFollowPos;
    [SerializeField] float mouseSense = 1f;

    //
    public AimBaseState currState;
    public HipFireState Hip = new();
    public AimState Aim = new();

    [SerializeField] public CinemachineVirtualCamera aimCam;

    [SerializeField] LayerMask aimColliderLayerMask = new();

    [SerializeField] Transform t_debug;

    //=============================================================================
    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(hasFocus);
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerInput = GetComponent<UltimatePlayerInput>();


        SwitchState(Hip);
    }


    void LateUpdate()
    {
        //
        xAxis += playerInput.mouseMoveH * mouseSense;
        yAxis -= playerInput.mouseMoveV * mouseSense;
        yAxis = Mathf.Clamp(yAxis, -80, 80);        

        //
        Transform t_hit = null; // 히트스캔에 필요.
        // 조준점이 가리키는 화면 좌표구하기. 
        Vector3 mouseWorldPosition;
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width *0.5f, Screen.height * 0.5f) );  //조준점 위치(화면중앙));
        // 조준점 방향 계산
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f)) //, aimColliderLayerMask
        {
            mouseWorldPosition =  raycastHit.point;
            

            t_hit = raycastHit.transform;
        }
        else
        {
            mouseWorldPosition = ray.GetPoint(50); // 적절한 거리로 설정
        }

        t_debug.position = mouseWorldPosition;
            
        //
        currState.UpdateState(this);
    // }

    // void LateUpdate()
    // {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y,camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    //=============================================================================

    public void SwitchState(AimBaseState newState)
    {
        currState = newState;
        currState.EnterState(this);
    }
    
    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }


}
