using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class UltimatePlayerNewInput : MonoBehaviour
{
    PlayerInput playerInput;
    
    
    InputAction moveAction;
    InputAction lookAction;
    InputAction jumpAction;
    InputAction aimAction;

    //
    public Vector2 moveVector {get;private set;}

    public bool jump {get{return jumpAction.triggered;}}

    public bool aim { get ;private set;}


    //================================================================
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
        aimAction = playerInput.actions["Aim"];
    }

    void Update()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        aim = aimAction.ReadValue<float>()>0;

        if (aim)
        Debug.Log("에임");
    }
}
