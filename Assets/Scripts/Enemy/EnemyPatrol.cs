using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPatrol : MonoBehaviour
{
    public float patrolDistance = 3;
    public float speed = 2;

    [HideInInspector] 
    public float dir = 1f;
    float pointA, pointB;
    bool move;

    Rigidbody2D rb2d;
    EnemyFire enemyFire;

    public SpriteRenderer heartBubbleSpriteRenderer;

    public enum PatrolMode
    {
        Hostile,
        Peaceful
    }
    public PatrolMode currentPatrolMode;


    void Start()
    {
        currentPatrolMode = PatrolMode.Hostile;

        pointA = transform.position.x + -patrolDistance;
        pointB = transform.position.x + patrolDistance;

        rb2d = GetComponent<Rigidbody2D>();
        enemyFire = GetComponent<EnemyFire>();

        StartCoroutine(RandomMove());
    }
    void Update()
    {
        if (!enemyFire.playerDetected)
        {
            if (move)
            {
                rb2d.velocity = new Vector2(speed * dir, rb2d.velocity.y);

                if (transform.position.x >= pointB)
                {
                    DirectionFlip(-1);
                }
                if (transform.position.x <= pointA)
                {
                    DirectionFlip(1);
                }
            }
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
    }

    private void RandomDir()
    {
        if (Random.Range(0, 2) == 0)
        {
            DirectionFlip(-1);
        }
        else
        {
            DirectionFlip(1);
        }
    }

    private void DirectionFlip(float value)
    {
        dir = value;
        transform.localScale = new Vector3(1 * value, 1, 1);
    }
    private IEnumerator RandomMove()
    {
        if (!enemyFire.playerDetected)
        {
            move = true;
            RandomDir();
        }

        yield return new WaitForSeconds(Random.Range(0.5f, 3f));
        rb2d.velocity = new Vector2(0, rb2d.velocity.y);

        move = false;

        yield return new WaitForSeconds(Random.Range(1f, 3f));

        StartCoroutine(RandomMove());
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector2(pointA, transform.position.y), 0.1f);
        Gizmos.DrawWireSphere(new Vector2(pointB, transform.position.y), 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FlowerExpslosion")
        {
            Debug.Log("HIT ENEMY");
            currentPatrolMode = PatrolMode.Peaceful;
            heartBubbleSpriteRenderer.enabled = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SingleFlowerProjectile")
        {
            Debug.Log("HIT ENEMY");
            currentPatrolMode = PatrolMode.Peaceful;
            heartBubbleSpriteRenderer.enabled = true;
        }
    }
}
