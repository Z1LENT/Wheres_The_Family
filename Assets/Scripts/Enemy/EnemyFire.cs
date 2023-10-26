using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    Vector2 raycastDirection;

    public bool playerDetected;
    bool previousDetectionState;
    bool alreadyExiting;

    float detectionDistance = 8f;
    float cooldownTime = 2;
    float timeOfNextFire;
    float initialWaitTime = 1f;
    float exitDetectionTime = 2;
    //float alertBubbleTime = 2;

    public GameObject projectile;
    public GameObject explosion;
    public Transform bulletSpawnPoint;
    
    EnemyPatrol enemyPatrol;
    EnemyAnimations enemyAnimations;
    public SpriteRenderer alertBubbleSpriteRenderer;

    void Start()
    {
        enemyPatrol = GetComponent<EnemyPatrol>();
        enemyAnimations = GetComponent<EnemyAnimations>();
        alertBubbleSpriteRenderer.enabled = false;

    }

    void Update()
    {
        PlayerCheck();

        if (playerDetected && enemyPatrol.currentPatrolMode == EnemyPatrol.PatrolMode.Hostile)
        {
            Fire();
            alertBubbleSpriteRenderer.enabled = true;
        }
        else
        {
            alertBubbleSpriteRenderer.enabled = false;
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
                    alreadyExiting = false;
                    return;
                }
            }
        }

        if(playerDetected)
        {
            if (!alreadyExiting)
            {
                StartCoroutine(ExitDetectionState());
            }
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
            if (enemyPatrol.dir == 1)
            {
                Instantiate(projectile, bulletSpawnPoint.transform.position, Quaternion.Euler(0, 0, 0));
                GameObject newExplosion = Instantiate(explosion, bulletSpawnPoint.transform.position, Quaternion.identity);
                Destroy(newExplosion, 0.1f);
            }
            else
            {
                Instantiate(projectile, bulletSpawnPoint.transform.position, Quaternion.Euler(0, 0, -180));
                GameObject newExplosion = Instantiate(explosion, bulletSpawnPoint.transform.position, Quaternion.identity);
                Destroy(newExplosion, 0.1f);
            }
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
