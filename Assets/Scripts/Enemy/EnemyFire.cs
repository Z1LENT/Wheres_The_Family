using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    //TODO check default facing direction. assumes it is on the left atm.

    Vector2 currentPos;

    bool playerDetected;

    float detectionDistance = 8f;
    float timeOfNextPlayerCheck;
    float checkInterval = 2;

    float timeOfNextFire;
    float cooldownTime;

    public GameObject projectile;

    void Update()
    {
        if (playerDetected)
        {
            if (Time.time > timeOfNextPlayerCheck)
            {
                timeOfNextPlayerCheck = Time.time + checkInterval;
                PlayerCheck();
            }
        }
        else
        {
            PlayerCheck();
        }
    }

    private void PlayerCheck()
    {
        print("player check");
        currentPos = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D[] hit = Physics2D.RaycastAll(currentPos, -transform.right, detectionDistance);

        Debug.Log(hit.Length);

        if (hit.Length > 0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.CompareTag("Player"))
                {
                    Fire();
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
}
