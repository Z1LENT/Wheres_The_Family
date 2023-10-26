using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerParticle : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(nameof(Remove));
    }
    IEnumerator Remove()
    {
        yield return new WaitForSeconds(0.1f);

        if(GetComponent<CircleCollider2D>() != null)
            GetComponent<CircleCollider2D>().enabled = false;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Enemy")
    //    {

    //    }
    //}

}
