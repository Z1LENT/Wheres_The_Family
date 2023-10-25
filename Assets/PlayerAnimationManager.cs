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
        Jump,
        Fall
    }
    public PlayerAnimationState currentAnimationState;

    private void Start()
    {
        currentAnimationState = PlayerAnimationState.Idle;
    }

    public void SetAnimationToIdle()
    {
        if(currentAnimationState == PlayerAnimationState.Fall)
        {
            currentAnimationState = PlayerAnimationState.Idle;
            animator.SetBool("Walk", false);
            animator.SetBool("Jump", false);
        }
        if (currentAnimationState == PlayerAnimationState.Walk)
        {
            currentAnimationState = PlayerAnimationState.Idle;
            animator.SetBool("Walk", false);
        }

    }
    public void SetAnimationToWalk()
    {
        if(currentAnimationState != PlayerAnimationState.Idle) { return; }
        currentAnimationState = PlayerAnimationState.Walk;
        animator.SetBool("Walk", true);
    }

    public void StartJumping()
    {
        if (currentAnimationState != PlayerAnimationState.Jump)
        {

            currentAnimationState = PlayerAnimationState.Jump;
            animator.SetBool("Jump", true);
        }
    }

    public void ToggleJumpAnimation() //called from jump animation too
    {
        if (currentAnimationState == PlayerAnimationState.Jump)
        {
            currentAnimationState = PlayerAnimationState.Fall;
        }
        else
        {
        }
    }

    public void LandAnimation()
    {
        if(currentAnimationState == PlayerAnimationState.Fall)
        {
            animator.SetBool("Jump", false);
            SetAnimationToIdle();
        }
    }


    public void SetAnimationToKilled()
    {
        animator.SetBool("Killed", true);
    }

}
