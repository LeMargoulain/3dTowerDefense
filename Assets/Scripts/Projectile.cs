using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float range = 20f;
    public float speed = 15f;
    public float damage = 5f;
    public LayerMask enemyLayer;
    private GameObject enemyTarget;
    private Vector3 direction;
    private EnemyHealthSystem targetHealthSystem;
    void Start()
    {
        enemyTarget = ClosestEnemy();
    }
    void Update()
    {
        if (EnemyInGame())
        {
            GoToTarget();
        }
        else Destroy(gameObject);
    }

    private GameObject ClosestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, enemyLayer);

        if (colliders.Length == 0)
        {
            return null;
        }
        float distMin = Vector3.Distance(transform.position, colliders[0].gameObject.transform.position);
        GameObject closestEnemy = colliders[0].gameObject;
        foreach (Collider collider in colliders)
        {
            if (Vector3.Distance(transform.position, collider.gameObject.transform.position) < distMin)
            {
                distMin = Vector3.Distance(transform.position, collider.gameObject.transform.position);
                closestEnemy = collider.gameObject;
            }
        }
        targetHealthSystem = closestEnemy.GetComponent<EnemyHealthSystem>();
        return closestEnemy;
    }

    private void GoToTarget()
    {
        if (enemyTarget == null)
        {
            enemyTarget = ClosestEnemy();
            if (enemyTarget == null)
            {
                Destroy(gameObject);
                return;
            }
        }
        direction = (enemyTarget.transform.position - gameObject.transform.position).normalized;
        transform.position = transform.position + direction * speed * Time.deltaTime;

        if (Vector3.Magnitude(enemyTarget.transform.position - gameObject.transform.position) < 0.1f && targetHealthSystem != null)
        {
            targetHealthSystem.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private bool EnemyInGame()
    {
        return (GameObject.FindWithTag("Enemy") != null);
    }

}
