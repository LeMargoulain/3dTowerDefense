using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float range = 20f;
    public float speed = 15f;
    public LayerMask enemyLayer;

    private Transform enemyTarget;
    private Vector3 direction;
    void Start()
    {
        enemyTarget = ClosestEnemy();
        direction = (enemyTarget.position - gameObject.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        GoToTarget();
    }

    private Transform ClosestEnemy()
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
        return closestEnemy.transform;
    }

    private void GoToTarget()
    {
        direction = (enemyTarget.position - gameObject.transform.position).normalized;
        transform.position = transform.position + direction * speed * Time.deltaTime;

        if (Vector3.Magnitude(enemyTarget.position - gameObject.transform.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

}
