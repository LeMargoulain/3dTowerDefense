using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;
    EnemyUI healthBar;
    private Spawner mySpawner;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<EnemyUI>();
        healthBar.SetMaxHealth(maxHealth);
        mySpawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {

    }

    public IEnumerator TakeDamage(float damage)
    {
        // introduce a delay of 0.5 seconds

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            if (gameObject.GetComponent<SlimeDivision>() != null)
            {
                SlimeDivision tospawn = gameObject.GetComponent<SlimeDivision>();
                tospawn.SpawnAfterDeath();
            }
            mySpawner.OnMonsterDestroyed();
            Destroy(gameObject);
            GameManager.AddMoney(10);
        }
        yield return new WaitForSeconds(0.05f);
    }
}

