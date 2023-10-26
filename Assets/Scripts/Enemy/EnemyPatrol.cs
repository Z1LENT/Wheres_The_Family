using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPatrol : MonoBehaviour
{
    public float patrolDistance = 3;
    public float speed = 2;
    public bool doNotMove;

    [HideInInspector] 
    public float dir = 1f;
    float pointA, pointB;
    bool move;

    Rigidbody2D rb2d;
    EnemyFire enemyFire;


    public Image peaceImage;
    public float maxPeaceResistance;
    private float currentPeaceResistance;

    public GameObject bubbleCanvas;

    public enum PatrolMode
    {
        Hostile,
        Peaceful
    }
    public PatrolMode currentPatrolMode;


    void Start()
    {
        currentPeaceResistance = 0;


        dir = transform.localScale.x;


        currentPatrolMode = PatrolMode.Hostile;

        pointA = transform.position.x + -patrolDistance;
        pointB = transform.position.x + patrolDistance;

        rb2d = GetComponent<Rigidbody2D>();
        enemyFire = GetComponent<EnemyFire>();

        if (doNotMove) { return; }

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

    public GameObject peaceExplosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FlowerExpslosion")
        {
            if (currentPatrolMode == PatrolMode.Peaceful) { return; }


            currentPeaceResistance = maxPeaceResistance;
            GetComponent<EnemyFire>().alertBubbleSpriteRenderer.gameObject.SetActive(false);
            bubbleCanvas.SetActive(true);

            currentPatrolMode = PatrolMode.Peaceful;
            GameObject peace = Instantiate(peaceExplosion, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(peace, 1f);
            peaceImage.fillAmount = currentPeaceResistance / maxPeaceResistance;

            Debug.Log("HIT ENEMY");
            currentPatrolMode = PatrolMode.Peaceful;
            GameObject peace1 = Instantiate(peaceExplosion, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(peace1, 1f);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SingleFlowerProjectile")
        {
            if (currentPatrolMode == PatrolMode.Peaceful) { return; }

            Destroy(collision.gameObject);

            GetComponent<EnemyFire>().alertBubbleSpriteRenderer.gameObject.SetActive(false);

            bubbleCanvas.SetActive(true);
            currentPeaceResistance++;

            GetComponent<EnemyFire>().alertBubbleSpriteRenderer.enabled = false;

            if(currentPeaceResistance >= maxPeaceResistance)
            {
                GetComponent<EnemyFire>().alertBubbleSpriteRenderer.gameObject.SetActive(false);

                currentPatrolMode = PatrolMode.Peaceful;
                GameObject peace = Instantiate(peaceExplosion, transform.position, Quaternion.Euler(0, 0, 0));
                Destroy(peace, 1f);
            }

            peaceImage.fillAmount = currentPeaceResistance / maxPeaceResistance;

            Debug.Log("HIT ENEMY");
        }
    }
}
