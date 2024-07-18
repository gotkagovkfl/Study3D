using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    public Animator animator;
    
    public float xAxis, yAxis;
    [SerializeField] Transform camFollowPos;
    [SerializeField] float mouseSense = 1f;

    //
    public AimBaseState currState;
    public HipFireState Hip = new();
    public AimState Aim = new();

    [SerializeField] public CinemachineVirtualCamera aimCam;



    //=============================================================================
    void Start()
    {
        animator = GetComponentInChildren<Animator>();


        SwitchState(Hip);
    }


    void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;

        yAxis = Mathf.Clamp(yAxis, -80, 80);


        currState.UpdateState(this);
    }

    void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y,camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    //=============================================================================

    public void SwitchState(AimBaseState newState)
    {
        currState = newState;
        currState.EnterState(this);
    }
    


}
