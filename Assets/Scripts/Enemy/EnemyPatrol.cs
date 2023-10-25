using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPatrol : MonoBehaviour
{
    float distance = 3;
    float speed = 2;
    
    public float dir = 1f;
    float pointA, pointB;
    bool move;

    Rigidbody2D rb2d;
    EnemyFire enemyFire;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        pointA = transform.position.x + -distance;
        pointB = transform.position.x + distance;

        rb2d = GetComponent<Rigidbody2D>();
        enemyFire = GetComponent<EnemyFire>();
        spriteRenderer = GetComponent<SpriteRenderer>();
 

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
        move = true;
        RandomDir();

        yield return new WaitForSeconds(Random.Range(0.5f, 3f));

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
}
