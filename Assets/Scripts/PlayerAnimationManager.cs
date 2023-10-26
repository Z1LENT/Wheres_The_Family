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
        Fall,
        StartHurt,
        EndHurt,
        Climb,
        Killed
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
        if(currentAnimationState == PlayerAnimationState.EndHurt)
        {
            Debug.Log("HERE");
            currentAnimationState = PlayerAnimationState.Idle;
            animator.SetBool("Walk", false);
            animator.SetBool("Jump", false);
            animator.SetBool("Hurt", false);
        }
        if (currentAnimationState == PlayerAnimationState.Climb)
        {
            Debug.Log("HERE");
            currentAnimationState = PlayerAnimationState.Idle;
            animator.SetBool("Climb", false);
            animator.SetBool("Hurt", false);
        }
    }
    public void SetAnimationToWalk()
    {
        if(currentAnimationState == PlayerAnimationState.Idle || currentAnimationState == PlayerAnimationState.EndHurt) 
        {
            currentAnimationState = PlayerAnimationState.Walk;
            animator.SetBool("Walk", true);
            animator.SetBool("Hurt", false);
        }
    }

    public void StartJumping()
    {
        currentAnimationState = PlayerAnimationState.Jump;
        animator.SetBool("Jump", true);

    }

    public void StartFallAnimation() //called from jump animation too
    {
        if (currentAnimationState == PlayerAnimationState.Jump)
        {
            currentAnimationState = PlayerAnimationState.Fall;
        }
        else
        {
            animator.SetBool("Jump", false);
            currentAnimationState = PlayerAnimationState.Idle;
        }
    }

    public void LandAnimation() //Called always when we are on the ground
    {
        if(currentAnimationState == PlayerAnimationState.Fall)
        {
            animator.SetBool("Jump", false);
            SetAnimationToIdle();
        }
    }

    public void SetAnimationToKilled()
    {
        currentAnimationState = PlayerAnimationState.Killed;
        animator.SetBool("Killed", true);
    }
    public void SetAnimationToStartHurt()
    {
        currentAnimationState = PlayerAnimationState.StartHurt;
        animator.SetBool("Hurt", true);
    }
    public void SetAnimationToEndHurt()
    {
        currentAnimationState = PlayerAnimationState.EndHurt;
    }

    public void SetAnimationToClimb()
    {
        animator.SetBool("Climb", true);
        animator.SetBool("Hurt", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Jump", false);

        currentAnimationState = PlayerAnimationState.Climb;
    }
}
