using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public float Speed = 4;
    public Vector2 LaunchOffset;
    public bool Thrown;

    public Vector2 direction = new Vector2(1, 1);
    [SerializeField] bool isVase;
    [SerializeField] private GameObject flowerExplosionPrefab;
    private void Start()
    {
        if (Thrown)
        {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isVase)
        {
            GameObject flowerExplosion = Instantiate(flowerExplosionPrefab, transform.position, transform.rotation);
            Destroy(flowerExplosion, 2);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            //TODO enemy ska lukta på blomma, inte bry sig om player
        }

    }

}
