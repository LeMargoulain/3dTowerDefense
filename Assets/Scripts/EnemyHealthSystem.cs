using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;

    EnemyUI healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<EnemyUI>();
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            GameManager.AddMoney(5);

        }
    }
}
