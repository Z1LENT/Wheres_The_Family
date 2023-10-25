using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float Speed = 4;
    public Vector2 LaunchOffset;
    public bool Thrown;

    private void Start()
    {

        if (Thrown)
        {
            var direction = transform.right;
            GetComponent<Rigidbody2D>().AddForce(direction * Speed, ForceMode2D.Impulse);
        }
        transform.Translate(LaunchOffset);

        Destroy(gameObject, 5);
    }

    public void Update()
    {
        if (!Thrown)
        {
            transform.position += transform.right * Speed * Time.deltaTime;
        }
    }

    private void OnColliderEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //TODO enemy ska lukta på blomma, inte bry sig om player
            Destroy(gameObject);
        }
    }
}
