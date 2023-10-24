using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    //TODO check default facing direction. assumes it is on the left atm.
    //TODO add delay before firing, add delay before re-entering patrol.

    Vector2 currentPos;
    Vector2 raycastDirection;

    public bool playerDetected;

    float detectionDistance = 8f;
    float timeOfNextPlayerCheck;
    float checkInterval = 2;

    float timeOfNextFire;
    float cooldownTime = 2;

    public GameObject projectile;
    EnemyPatrol enemyPatrol;

    private void Start()
    {
        enemyPatrol = GetComponent<EnemyPatrol>();
    }

    void Update()
    {
        PlayerCheck();
    
        if (playerDetected)
        {
            Fire();
        }
        
    }

    private void PlayerCheck()
    {
        raycastDirection = enemyPatrol.dir * transform.right;
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, raycastDirection, detectionDistance);

        Debug.Log(hit.Length);

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
                    playerDetected = false;
                    //TODO MOVE 
                }
            }
        }
        else 
        {
            playerDetected = false;
            //TODO MOVE 
        }
    }
    void Fire()
    {
        if (Time.time > timeOfNextFire)
        {
            Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, 90));
            timeOfNextFire = Time.time + cooldownTime;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, 0.2f);
        //Gizmos.DrawWireSphere(raycastDirection * detectionDistance, 0.2f);

        Gizmos.DrawRay(transform.position, raycastDirection * detectionDistance);
    }
    
}
