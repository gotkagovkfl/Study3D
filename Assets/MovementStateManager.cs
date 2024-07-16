using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MovementStateManager : MonoBehaviour
{
    //
    [SerializeField] public Animator animator {get;private set;}
    [SerializeField] CharacterController _controller;
    Vector3 velocity;
    [SerializeField] float gravity = -9.81f;

    public float hInput, vInput;
    public Vector3 dir;


    public float currMoveSpeed ;
    public float walkSpeed = 3f, walkBackSpeed =2f;
    public float runSpeed = 7f, runBackSpeed = 5f;
    public float crouchSpeed = 2f, crouchBackSpeed = 1f;
    
    
    // 중력 관련. ( rb 안쓰는 이유?? )
    [SerializeField]  float groundYOffset;
    [SerializeField]  LayerMask groundMask;
    Vector3 spherePos;  // 그라운드 체크를 위함.


    //
    MovementBaseState currState;
    public IdleState idleState = new();
    public WalkState walkState = new();
    public CrouchState crouchState = new();
    public RunState runState = new();

    //==================================================================================================================
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();    

        SwitchState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();


        animator.SetFloat("hzInput",hInput);
        animator.SetFloat("vInput",vInput);
        currState.UpdateState(this);
    }

    //==================================================================================================================
    public void SwitchState(MovementBaseState newState)
    {
        currState = newState;
        currState.EnterState(this);
    }
    
    
    
    
    void GetDirectionAndMove()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        // if( hInput !=0)
        // {
        //     hInput = hInput>0? 1:-1;
        // }
        // if( vInput !=0)
        // {
        //     vInput = vInput>0? 1:-1;
        // }


        dir = transform.forward * vInput + transform.right * hInput;

        _controller.Move( dir.normalized * currMoveSpeed * Time.deltaTime );
    }

    void Gravity()
    {
        if (!IsGrounded)
        {
            velocity.y  += gravity * Time.deltaTime;
        }
        else if (velocity.y < 0)
        {
            velocity.y = -2;
        }


        _controller.Move( velocity *Time.deltaTime );
    }


    bool IsGrounded
    {
        get
        {
            spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
            if(Physics.CheckSphere(spherePos, _controller.radius - 0.05f, groundMask ))
            {
                return true;
            }
            return false;

        }
    }



    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, _controller.radius - 0.05f);
    }
}
