using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class UltimatePlayerNewInput : MonoBehaviour
{
    PlayerInput playerInput;
    
    
    InputAction moveAction;
    InputAction lookAction;
    InputAction jumpAction;

    //
    public Vector2 moveVector {get;private set;}

    public bool jump {get{return jumpAction.triggered;}}


    //================================================================
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
    }

    void Update()
    {
        moveVector = moveAction.ReadValue<Vector2>();
    }
}
