using System.Resources;
using Unity.VisualScripting;
using UnityEngine;

public abstract class MovementBaseState 
{
    public abstract void EnterState(MovementStateManager move);
    

    public abstract void UpdateState(MovementStateManager move);
    

    
}

//===================================================================================

public class IdleState : MovementBaseState
{
    public override void EnterState(MovementStateManager move)
    {

    }

    public override void UpdateState(MovementStateManager move)
    {
        if (move.IsMoving)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                move.SwitchState(move.runState);
            }
            else
            {
                move.SwitchState(move.walkState);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            move.SwitchState(move.crouchState);
        }
    }
}

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager move)
    {
        move.animator.SetBool("Walking",true);
    }

    public override void UpdateState(MovementStateManager move)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(move, move.runState);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            ExitState(move, move.crouchState);
        }
        else if (!move.IsMoving)
        {
            ExitState(move, move.idleState);
        }

        if( move.IsFoward) move.currMoveSpeed = move.walkSpeed;
        else move.currMoveSpeed = move.walkBackSpeed;
    }

    public void ExitState(MovementStateManager move, MovementBaseState state)
    {
        move.animator.SetBool("Walking", false);
        move.SwitchState(state);
    }

}

public class CrouchState : MovementBaseState
{
    public override void EnterState(MovementStateManager move)
    {
        move.animator.SetBool("Crouching",true);
    }

    public override void UpdateState(MovementStateManager move)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(move, move.runState);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            if (!move.IsMoving)
                ExitState(move, move.idleState);
            else 
            {
                ExitState(move, move.walkState);
            }
        }

        if( move.IsFoward ) move.currMoveSpeed = move.crouchSpeed;
        else move.currMoveSpeed = move.crouchBackSpeed;
    }

        public void ExitState(MovementStateManager move, MovementBaseState state)
    {
        move.animator.SetBool("Crouching", false);
        move.SwitchState(state);
    }
}

public class RunState : MovementBaseState
{
    public override void EnterState(MovementStateManager move)
    {
        move.animator.SetBool("Running",true);
    }

    public override void UpdateState(MovementStateManager move)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ExitState(move, move.walkState);
        }
        else if (!move.IsMoving)
        {
            ExitState(move, move.idleState);
        }

        
        if( move.IsFoward) move.currMoveSpeed  = move.runSpeed;
        else move.currMoveSpeed= move.runBackSpeed;

    }
    public void ExitState(MovementStateManager move, MovementBaseState state)
    {
        move.animator.SetBool("Running", false);
        move.SwitchState(state);
    }
}
