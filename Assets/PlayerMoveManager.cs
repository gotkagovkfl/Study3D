using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerMoveManager : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Vector3 dir;
    float _hInput, _vInput;
    [SerializeField] CharacterController _controller;

    [SerializeField]  float groundYOffset;
    [SerializeField]  LayerMask groundMask;
    Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
    }


    void GetDirectionAndMove()
    {
        _hInput = Input.GetAxis("Horizontal");
        _vInput = Input.GetAxis("Vertical");

        dir = (transform.forward * _vInput + transform.right * _hInput).normalized;

        _controller.Move( dir* moveSpeed * Time.deltaTime );
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
