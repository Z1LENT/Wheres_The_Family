using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyAnimations>() != null)
        {
            other.GetComponent<EnemyAnimations>().Hit();
            print("hit");
        }
    }
}
