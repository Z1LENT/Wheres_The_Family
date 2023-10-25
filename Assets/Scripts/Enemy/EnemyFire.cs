using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    Vector2 raycastDirection;

    public bool playerDetected;
    bool previousDetectionState;

    float detectionDistance = 8f;
    float cooldownTime = 2;
    float timeOfNextFire;
    float initialWaitTime = 1f;
    float exitDetectionTime = 1.5f;
    //float alertBubbleTime = 2;

    public GameObject projectile;
    
    EnemyPatrol enemyPatrol;
    EnemyAnimations enemyAnimations;
    SpriteRenderer bubbleSpriteRenderer;

    void Start()
    {
        enemyPatrol = GetComponent<EnemyPatrol>();
        enemyAnimations = GetComponent<EnemyAnimations>();

        Transform childTransform = transform.Find("Bubble");
        bubbleSpriteRenderer = childTransform.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerCheck();
    
        if (playerDetected)
        {
            Fire();
            bubbleSpriteRenderer.enabled = true;
        }
        else
        {
            bubbleSpriteRenderer.enabled = false;
        }

        previousDetectionState = playerDetected;
    }

    private void PlayerCheck()
    {
        raycastDirection = enemyPatrol.dir * transform.right;
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, raycastDirection, detectionDistance);

        if (hit.Length > 0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.CompareTag("Player")) 
                {
                    playerDetected = true;
                    return;
                }
                else
                {
                    StartCoroutine(ExitDetectionState());
                }
            }
        }
        else 
        {
            StartCoroutine(ExitDetectionState());
        }
    }

    void Fire()
    {
        if (previousDetectionState != playerDetected)
        {
            timeOfNextFire = Time.time + initialWaitTime;
            //StartCoroutine(AlertBubble());
        }

        if (Time.time > timeOfNextFire)
        {
            Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, -90 * enemyPatrol.dir));
            timeOfNextFire = Time.time + cooldownTime;
            enemyAnimations.Fire();
        }
    }

    IEnumerator ExitDetectionState()
    {
        yield return new WaitForSeconds(exitDetectionTime);
        playerDetected = false;
    }

    //IEnumerator AlertBubble()
    //{
    //    alertSpriteRenderer.enabled = true;
    //    yield return new WaitForSeconds(alertBubbleTime);
    //    alertSpriteRenderer.enabled = false;
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, raycastDirection * detectionDistance);
    }
    
}
