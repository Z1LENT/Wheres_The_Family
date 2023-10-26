using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    float timeVisible = 0.1f;
    float timeAlive = 2;

    void Start()
    {
        Invoke("DisableSprite", timeVisible);
        Destroy(gameObject, timeAlive);
    }
    void DisableSprite()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
