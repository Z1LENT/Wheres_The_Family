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
            Debug.Log("SET TO IDLE");
            currentAnimationState = PlayerAnimationState.Idle;
            animator.SetBool("Walk", false);
            animator.SetBool("Jump", false);
        }
        if (currentAnimationState == PlayerAnimationState.Walk)
        {
            Debug.Log("SET TO IDLE HERE");
            currentAnimationState = PlayerAnimationState.Idle;
            animator.SetBool("Walk", false);
            //animator.SetBool("Jump", false);
        }

    }
    public void SetAnimationToWalk()
    {
        if(currentAnimationState != PlayerAnimationState.Idle) { return; }
        Debug.Log("WALK");
        currentAnimationState = PlayerAnimationState.Walk;
        animator.SetBool("Walk", true);
    }

    public void StartJumping()
    {
        if (currentAnimationState != PlayerAnimationState.Jump)
        {
            Debug.Log("start jumping");

            currentAnimationState = PlayerAnimationState.Jump;
            animator.SetBool("Jump", true);
        }
    }

    public void ToggleJumpAnimation() //called from jump animation too
    {
        if (currentAnimationState == PlayerAnimationState.Jump)
        {
            Debug.Log("Start falling");
            currentAnimationState = PlayerAnimationState.Fall;
        }
        else
        {
            Debug.Log("WAS NOT JUMP");
        }
    }

    public void LandAnimation()
    {
        if(currentAnimationState == PlayerAnimationState.Fall)
        {
            animator.SetBool("Jump", false);
            Debug.Log("ended jumping");
            SetAnimationToIdle();
        }
    }


    public void SetAnimationToKilled()
    {
        animator.SetBool("Killed", true);
    }

}
