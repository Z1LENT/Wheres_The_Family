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

        if (Input.GetMouseButton(0) && timer <= 0f)
        {
            Shoot();
            timer = fireRate;
        }
    }

    private void Shoot()
    {
        Instantiate(flowerPrefab, ThrowPoint.position, ThrowPoint.rotation);
    }
}
