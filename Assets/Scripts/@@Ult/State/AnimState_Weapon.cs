using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;



public class AnimState_Weapon : StateMachineBehaviour
{
    [SerializeField] AnimationUtil animationUtil;
    [SerializeField] WeaponType weaponType;
    // [SerializeField] bool isHolster;
    

    int hash_holding = Animator.StringToHash("Holding");

    //============================================================================

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        if(!animationUtil)
        {
            animationUtil = animator.GetComponent<AnimationUtil>();
        }


        // 
        if ( animator.GetBool(hash_holding) )
        {
            Debug.Log($"홀드 애니메이션 스테이트 {weaponType}  ");
            animationUtil.OnHold(weaponType);
            
        }
        else
        {
            Debug.Log($"홀스터 애니메이션 스테이트 {weaponType}  ");
            animationUtil.OnHolster(weaponType);
        }        
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

}