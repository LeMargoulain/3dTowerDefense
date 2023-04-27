using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Projectile projectile;
    private float delayBetweenShot;
    public Transform firingPosition;
    public LayerMask enemyLayer;
    public bool isShooting;
    private bool enemyInRange;

    public Projectile[] projectiles;

    void Start()
    {
        projectile = projectiles[Player.GetGunNumber()];
        delayBetweenShot = projectile.delay;
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        IsEnemyInRange();
        if (enemyInRange && !isShooting)
        {
            StartCoroutine(Shoot());
        }

    }

    public void ChangeProjectile(int newProjectileIndex)
    {
        projectile = projectiles[newProjectileIndex];
        delayBetweenShot = projectile.delay;
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        Instantiate(projectile, firingPosition);
        Debug.Log("shot");
        yield return new WaitForSeconds(delayBetweenShot);
        isShooting = false;
    }

    public void IsEnemyInRange()
    {
        enemyInRange = Physics.CheckSphere(firingPosition.position, projectile.range, enemyLayer);
    }

    public void SetProjectile(int index)
    {
        projectile = projectiles[index];
    }

}
