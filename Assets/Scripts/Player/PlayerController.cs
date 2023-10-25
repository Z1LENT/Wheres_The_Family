using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 7f;
    public float acceleration = 10f;
    public float deacceleration = 10f;

    internal float horizontal;
    internal float velocityX;

    [Header("Jump")]
    public float jumpHeight = 4.9f;
    public float downGravity = 2.5f;
    public float airAcceleration = 44f;
    public float airControl = 70f;
    public float airBrake = 19f;
    public float groundCheckDistance = 0.1f;

    [Header("Shoot")]
    float fireRate = 1f;
    float timer;

    float groundCheckLenght;
    bool isJumping = false;

    public ProjectileBehavior FlowerPrefab;
    public ProjectileBehavior VasePrefab;

    public Transform LaunchOffset;

    Rigidbody2D rb;
    [SerializeField] PlayerAnimationManager animationManager;


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

        Shoot();

        Flip();

        if (onGround)
        {
            isJumping = false;
        }
    }

    private void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (onGround)
        {
            velocityX += horizontal * acceleration * Time.deltaTime;
            velocityX = Mathf.Clamp(velocityX, -speed, speed);
            
            if(velocityX > 0.1 || velocityX < -0.1) { animationManager.SetAnimationToWalk(); }
            else { animationManager.SetAnimationToIdle(); }
        }
        else
        {
            float airAcce1 = isJumping ? airAcceleration : airBrake;
            velocityX = Mathf.MoveTowards(velocityX, horizontal * speed, airAcce1 * Time.deltaTime);
        }
        

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
            rb.gravityScale = downGravity;
        }
        else
            rb.gravityScale = 2f;
    }

    private void Jump()
    {
        if (onGround && Input.GetButtonDown("Jump"))
        {
            float jumpVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            isJumping = true;
            animationManager.ToggleJumpAnimation(true);
        }
        else { animationManager.ToggleJumpAnimation(false); }
    }

    bool onGround = true;

    private void FixedUpdate()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLenght);
    }

    private void Shoot()
    {
        timer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timer <= 0f)
        {
            Instantiate(FlowerPrefab, LaunchOffset.position, transform.rotation);
            timer = fireRate;
        }

        if (Input.GetMouseButtonDown(1) && timer <= 0f)
        {
            Instantiate(VasePrefab, LaunchOffset.position, transform.rotation);
            timer = fireRate;
        }
            
    }

    private void Flip()
    {
        float newScaleX = Mathf.Abs(transform.localScale.x);

        if (horizontal > 0 && newScaleX != transform.localScale.x)
        {
            transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
        }
        else if (horizontal < 0 && newScaleX != -transform.localScale.x)
        {
            transform.localScale = new Vector3(-newScaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}
