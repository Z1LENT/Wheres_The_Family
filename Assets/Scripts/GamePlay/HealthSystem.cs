using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    private Image HealthBar;

    public void Start()
    {
        HealthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
        if(HealthBar == null) { Debug.Log("NO HEALTHBAR"); }

        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(1);

        }


    }

    public void TakeDamage(int DamageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= DamageAmount;

            if (currentHealth < 0)
            {
                Death();
            }
        }

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if (HealthBar == null) { Debug.Log("NO HEALTHBAR"); return; }

        float fillAmount = (float)currentHealth / maxHealth;
        HealthBar.fillAmount = fillAmount;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
