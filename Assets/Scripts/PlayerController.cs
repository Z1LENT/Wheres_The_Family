using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float acceleration = 16f;
    public float deacceleration = 4f;

    internal float horizontal;
    internal float velocityX;

    [Header("Jump")]
    public float jumpingPower = 8f;
    public float groundCheckDistance = 0.1f;

    int maxJumps = 2;
    int currentJumps = 0;

    float groundCheckLenght;

    bool onGround = true;


    [Header("Dash")]

    [Header("WallJumping & WallSliding")]

    Rigidbody2D rb;

    private void Start()
    {
        Physics2D.queriesStartInColliders = false;

        rb = GetComponent<Rigidbody2D>();

        var collider = GetComponent<Collider2D>();
        groundCheckLenght = collider.bounds.size.y + groundCheckDistance;
    }

    private void Update()
    {
        Movement();

        Jump();

        GravityAdjust();
    }

    private void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        velocityX += horizontal * acceleration * Time.deltaTime;
        velocityX = Mathf.Clamp(velocityX, -speed, speed);

        if (horizontal == 0 || (horizontal < 0 == velocityX > 0))
        {
            velocityX *= 1 - deacceleration * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 8;
        }
        else
        {
            speed = 5;

        }

        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }

    private void GravityAdjust()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = 4;
        }
        else
            rb.gravityScale = 1;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && currentJumps < maxJumps)
        {
            currentJumps++;
            var velocity = rb.velocity;
            velocity.y = jumpingPower;
            rb.velocity = velocity;
            onGround = false;
            return;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.25f);
        }

        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLenght);

        if (onGround)
            currentJumps = 0;
    }

}
