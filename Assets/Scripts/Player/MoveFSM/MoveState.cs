using System.Resources;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MovementBaseState 
{
    public abstract void EnterState(MovementStateManager movementStateManager);
    

    public abstract void UpdateState(MovementStateManager movementStateManager);
    

    
}

//===================================================================================

public class IdleState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {

    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (movementStateManager.dir.sqrMagnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movementStateManager.SwitchState(movementStateManager.runState);
            }
            else
            {
                movementStateManager.SwitchState(movementStateManager.walkState);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            movementStateManager.SwitchState(movementStateManager.crouchState);
        }
    }
}

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.animator.SetBool("Walking",true);
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movementStateManager, movementStateManager.runState);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            ExitState(movementStateManager, movementStateManager.crouchState);
        }
        else if (movementStateManager.dir.sqrMagnitude < 0.1f)
        {
            ExitState(movementStateManager, movementStateManager.idleState);
        }

        if( movementStateManager.vInput < 0) movementStateManager.currMoveSpeed = movementStateManager.walkBackSpeed;
        else movementStateManager.currMoveSpeed = movementStateManager.walkSpeed;
    }

    public void ExitState(MovementStateManager movementStateManager, MovementBaseState state)
    {
        movementStateManager.animator.SetBool("Walking", false);
        movementStateManager.SwitchState(state);
    }

}

public class CrouchState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.animator.SetBool("Crouching",true);
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movementStateManager, movementStateManager.runState);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            if (movementStateManager.dir.sqrMagnitude < 0.1f)
                ExitState(movementStateManager, movementStateManager.idleState);
            else 
            {
                ExitState(movementStateManager, movementStateManager.walkState);
            }
        }

        if( movementStateManager.vInput < 0) movementStateManager.currMoveSpeed = movementStateManager.crouchBackSpeed;
        else movementStateManager.currMoveSpeed = movementStateManager.crouchSpeed;
    }

        public void ExitState(MovementStateManager movementStateManager, MovementBaseState state)
    {
        movementStateManager.animator.SetBool("Crouching", false);
        movementStateManager.SwitchState(state);
    }
}

public class RunState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.animator.SetBool("Running",true);
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ExitState(movementStateManager, movementStateManager.walkState);
        }
        else if (movementStateManager.dir.sqrMagnitude < 0.1f)
        {
            ExitState(movementStateManager, movementStateManager.idleState);
        }

        
        if( movementStateManager.vInput < 0) movementStateManager.currMoveSpeed = movementStateManager.runBackSpeed;
        else movementStateManager.currMoveSpeed = movementStateManager.runSpeed;

    }
    public void ExitState(MovementStateManager movementStateManager, MovementBaseState state)
    {
        movementStateManager.animator.SetBool("Running", false);
        movementStateManager.SwitchState(state);
    }
}
