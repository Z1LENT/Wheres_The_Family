using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    float distance = 3;
    float speed = 2;
    
    float dir = 1f;
    float pointA, pointB;
    bool move;

    Rigidbody2D rb2d;


    void Start()
    {
        pointA = transform.position.x + -distance;
        pointB = transform.position.x + distance;

        rb2d = GetComponent<Rigidbody2D>();

        StartCoroutine(RandomMove());
    }
    void Update()
    {
        if (move)
        {
            rb2d.velocity = new Vector2(speed * dir, rb2d.velocity.y);

            Debug.Log(pointA + " " + pointB);
            if (transform.position.x >= pointB)
            {
                dir = -1;
            }
            if (transform.position.x <= pointA)
            {
                dir = 1;
            }
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
    }

    private void RandomMoveDirection()
    {
        if (Random.Range(0, 2) == 0)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }
    }

    private IEnumerator RandomMove()
    {
        move = true;
        RandomMoveDirection();

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
