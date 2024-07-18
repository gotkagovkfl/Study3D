using System.Resources;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AimBaseState 
{
    public abstract void EnterState(AimStateManager aim);
    

    public abstract void UpdateState(AimStateManager aim);
    

    
}

//===================================================================================

public class HipFireState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.animator.SetBool("Aiming",false);
    }

    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKey(KeyCode.Mouse1)) 
        {
            aim.SwitchState(aim.Aim);
        }
    }
}


public class AimState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.animator.SetBool("Aiming",true);
    }

    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1)) 
        {
            aim.SwitchState(aim.Hip);
        }
    }
}
