using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;
    Animator bubbleAnimator;
    SpriteRenderer bubbleSpriteRenderer;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Transform childTransform = transform.Find("Bubble");

        bubbleSpriteRenderer = childTransform.GetComponent<SpriteRenderer>();
        bubbleAnimator = childTransform.GetComponent<Animator>();
    }

    void Update()
    {
        if (rb2d.velocity.x != 0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        //REMOVE ME !!!!
        if (Input.GetKeyDown(KeyCode.F8))
        {
            Hit();
        }
    }

    public void Fire()
    {
        animator.SetTrigger("Fire");
    }

    public void Hit()
    {
        GetComponent<EnemyFire>().enabled = false;
        GetComponent<EnemyPatrol>().enabled = false;
        
        animator.SetTrigger("Hit");
        bubbleSpriteRenderer.enabled = true;
        bubbleAnimator.Play("FlowerBubble");
    }
}
