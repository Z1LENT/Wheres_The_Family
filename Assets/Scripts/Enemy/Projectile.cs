using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8;
    public float lifeTime = 2;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;
        Destroy(gameObject, lifeTime);
    }
}
