using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        if (Input.GetKeyDown(KeyCode.P))
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
        animator.SetTrigger("Hit");
        FindObjectOfType<EnemyFire>().enabled = false;
        FindObjectOfType<EnemyPatrol>().enabled = false;
    }
}
