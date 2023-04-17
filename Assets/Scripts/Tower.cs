using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Projectile projectile;
    public float delayBetweenShot = 1f;
    public Transform firingPosition;
    public LayerMask enemyLayer;
    public bool isShooting;

    public Projectile[] projectiles;

    void Start()
    {
        projectile = projectiles[0];
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsEnemyInRange() && !isShooting)
        {
            StartCoroutine(Shoot());
        }

    }

    public void ChangeProjectile(int newProjectileIndex)
    {
        projectile = projectiles[newProjectileIndex];
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(firingPosition.position, projectile.range);
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        Instantiate(projectile, firingPosition);
        yield return new WaitForSeconds(delayBetweenShot);
        isShooting = false;
    }

    public bool IsEnemyInRange()
    {
        return Physics.CheckSphere(firingPosition.position, projectile.range, enemyLayer);
    }

}
