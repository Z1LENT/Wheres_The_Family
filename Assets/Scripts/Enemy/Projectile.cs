using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 12;
    public float lifeTime = 2;
    private Vector2 initialDirection;

    void Start()
    {
        initialDirection = transform.right;
        GetComponent<Rigidbody2D>().velocity = initialDirection * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        Destroy(gameObject);
    }
}
