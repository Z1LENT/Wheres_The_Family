using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject flowerPrefab;
    public Transform ThrowPoint;

    float fireRate = 1f;
    float timer;

    void Update()
    {
        timer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timer > fireRate)
        {
            timer = 0;
            Shoot();
        }

        timer = Time.deltaTime;
    }

    private void Shoot()
    {
        Instantiate(flowerPrefab, ThrowPoint.position, ThrowPoint.rotation);
    }
}
