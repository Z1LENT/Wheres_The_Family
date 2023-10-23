using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 7f;
    public float acceleration = 10f;
    public float deacceleration = 10f;

    internal float horizontal;
    internal float velocityX;

    [Header("Jump")]
    public float jumpingPower = 400f;
    public float groundCheckDistance = 0.1f;

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
            speed = 10;
        }
        else
        {
            speed = 7;

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
        if (onGround && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0f, jumpingPower));
            onGround = false;
            return;
        }

        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLenght);
    }

}
