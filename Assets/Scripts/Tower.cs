using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Projectile projectile;
    public float delayBetweenShot = 1f;
    public Transform firingPosition;
    public LayerMask enemyLayer;
    public bool isShooting;

    void Start()
    {
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

    public void ChangeProjectile(Projectile newProjectile)
    {
        projectile = newProjectile;
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
