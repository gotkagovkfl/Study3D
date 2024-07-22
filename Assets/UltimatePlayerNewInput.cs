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
    InputAction shootAction;

    //
    public Vector2 moveVector {get;private set;}

    public bool jump {get{return jumpAction.triggered;}}

    public bool aim { get ;private set;}

    public bool shoot {get;private set;}


    //================================================================
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
        aimAction = playerInput.actions["Aim"];
        shootAction = playerInput.actions["Shoot"];
    }

    void Update()
    {
        moveVector = moveAction.ReadValue<Vector2>();
        aim = aimAction.ReadValue<float>()>0;
        shoot = shootAction.ReadValue<float>()>0;

        // if (aim)
        // Debug.Log("에임");
    }

    // 커서고정
    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(hasFocus);
    }
    
    //=========================================================
    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
