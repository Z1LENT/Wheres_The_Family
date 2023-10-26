using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public Image HealthBar;

    public bool isPlayer = false;
    public bool isEnemy = false;

    public void Start()
    {
        isPlayer = CompareTag("Player");
        isEnemy = CompareTag("Enemy");
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPlayer)
        {
            TakeDamage(1);
        }
        
        if (isEnemy)
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

        if (!isEnemy)
            UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        float fillAmount = (float)currentHealth / maxHealth;
        HealthBar.fillAmount = fillAmount;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
