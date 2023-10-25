using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public enum PlayerAnimationState
    {
        Idle,
        Walk,
        Jump
    }
    public PlayerAnimationState currentAnimationState;

    private void Start()
    {
        currentAnimationState = PlayerAnimationState.Idle;
    }

    public void SetAnimationToIdle()
    {
        animator.SetBool("Walk", false);
    }
    public void SetAnimationToWalk()
    {




        if(currentAnimationState == PlayerAnimationState.Jump) { return; }
        Debug.Log("WALK");

        animator.SetBool("Walk", true);
    }

    public void ToggleJumpAnimation(bool jump)
    {
        if (jump)
        {
            currentAnimationState = PlayerAnimationState.Jump;
        }
        else
        {
            currentAnimationState = PlayerAnimationState.Walk;
        }
        animator.SetBool("Jump", jump);
    }

    public void SetAnimationToKilled()
    {
        animator.SetBool("Killed", true);
    }

}
