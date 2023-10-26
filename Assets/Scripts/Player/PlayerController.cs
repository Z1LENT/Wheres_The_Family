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
    float flowerFireRate = 0.5f;
    float flowerTimer;
    float vaseFireRate = 1.5f;
    float vaseTimer;


    public ProjectileBehavior FlowerPrefab;
    public ProjectileBehavior VasePrefab;

    public Transform LaunchOffset;

    Rigidbody2D rb;
    [SerializeField] PlayerAnimationManager animationManager;

    float groundCheckLenght;
    bool isJumping = false;
    bool onGround = true;
    /*[HideInInspector] */public bool facingRight;
    int fallAnimationCounter;

    private void Start()
    {
        hasVase = true;
        animationManager.SetVase(true);

        Physics2D.queriesStartInColliders = false;

        rb = GetComponent<Rigidbody2D>();

        var collider = GetComponent<Collider2D>();
        groundCheckLenght = collider.bounds.size.y + groundCheckDistance;

        facingRight = true;
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
            animationManager.LandAnimation();
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

            if(fallAnimationCounter > 20)
            {
                animationManager.currentAnimationState = PlayerAnimationManager.PlayerAnimationState.Idle;
                fallAnimationCounter = 0;
            }
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
            animationManager.StartJumping();
        }
    }

    private void FixedUpdate()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLenght);
    }

    public bool hasVase;

    private void Shoot()
    {
        flowerTimer -= Time.deltaTime;
        vaseTimer -= Time.deltaTime;

        //flower
        if (Input.GetMouseButtonDown(0) && flowerTimer <= 0f && hasVase)
        {
            ProjectileBehavior flower = Instantiate(FlowerPrefab, LaunchOffset.position, transform.rotation);
            if (!facingRight)
            {
                flower.direction = new Vector2(-flower.direction.x, flower.direction.y);
            }
            flowerTimer = flowerFireRate;

        }

        if (Input.GetMouseButtonDown(1) && vaseTimer <= 0f)
        {
            ProjectileBehavior vase = Instantiate(VasePrefab, LaunchOffset.position, transform.rotation);
            if (!facingRight)
            {
                vase.direction = new Vector2(-1, 1);
            }
            vaseTimer = vaseFireRate;
            hasVase = false;
            animationManager.SetVase(false);
        }

        if(vaseTimer <= 0)
        {
            hasVase = true;
            animationManager.SetVase(true);
        }

    }

    private void Flip()
    {
        float newScaleX = Mathf.Abs(transform.localScale.x);

        if (horizontal > 0 && newScaleX != transform.localScale.x)
        {
            transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
            facingRight = true;
        }
        else if (horizontal < 0 && newScaleX != -transform.localScale.x)
        {
            transform.localScale = new Vector3(-newScaleX, transform.localScale.y, transform.localScale.z);
            facingRight = false;
        }
    }
    // Försökte fixa knockback
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Player collided with SingleFlowerProjectile");
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            float knockbackForce = 3.0f;

            rb.velocity = new Vector2(knockbackDirection.x * knockbackForce, rb.velocity.y);
        }
    }

    public void IncreaseFallCounter()
    {
        fallAnimationCounter++;
    }
}
