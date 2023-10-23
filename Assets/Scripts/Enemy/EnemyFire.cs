using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    //TODO check default facing direction. assumes it is on the left atm.

    Vector2 currentPos;

    bool playerDetected;

    float detectionDistance = 3f;
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
        currentPos = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D[] hit = Physics2D.RaycastAll(currentPos, -transform.right, detectionDistance);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("Player"))
            {
                Fire();
                playerDetected = true;
            }
            else
            {
                playerDetected = false;
            }
        }
    }
    void Fire()
    {
        if (Time.time > timeOfNextFire)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeOfNextFire = Time.time + cooldownTime;
        }
    }
}
