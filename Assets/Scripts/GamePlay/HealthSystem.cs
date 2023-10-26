using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    //private Image HealthBar;

    public RawImage[] healthIcons;
    public PlayerAnimationManager animationManager;
    public AudioSource hitAudio;
    public GameObject gameOverScreen;
    PlayerController pc;
    public bool dead;

    public void Start()
    {
        //HealthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
        //if(HealthBar == null) { Debug.Log("NO HEALTHBAR"); }
        pc = GetComponent<PlayerController>();
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public float knockbackForce = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(1);

            Vector2 difference = (transform.position - other.transform.position).normalized;
            Vector2 force = difference * knockbackForce;

            pc.velocityX += force.x;

            //GetComponent<Rigidbody2D>().velocity.x = force.x; //if you don't want to take into consideration enemy's mass then use ForceMode.VelocityChange

        }
    }

    public void TakeDamage(int DamageAmount)
    {


        if (currentHealth > 0)
        {
            currentHealth -= DamageAmount;

            if (currentHealth <= 0)
            {
                Death();
            }
            else
            {
                animationManager.SetAnimationToStartHurt();
            }
        }
        
        hitAudio.Play();
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if(healthIcons.Length == 0) { return; }

        for (int i = 0; i < healthIcons.Length; i++)
        {
            if (currentHealth > i)
            {
                healthIcons[i].enabled = true;
            }
            else
            {
                healthIcons[i].enabled = false;
            }
        }
    }

    //public void UpdateHealthBar()
    //{
    //    if (HealthBar == null) { Debug.Log("NO HEALTHBAR"); return; }

    //    float fillAmount = (float)currentHealth / maxHealth;
    //    HealthBar.fillAmount = fillAmount;
    //}

    public void Death()
    {
        dead = true;
        animationManager.SetAnimationToKilled();
        StartCoroutine(nameof(DeathTime));
        //Destroy(gameObject);
    }

    IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(0.85f);
        gameOverScreen.SetActive(true);
    }
}
