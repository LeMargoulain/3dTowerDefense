using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public GameObject projectile;
    public Transform firingPosition;
    public Transform myCameraEyes;
    public float timeBetweenShot = 0.2f, gunDamage = 1f;
    private bool readyToShoot = true;
    public GameObject muzzleFlash;
    void Start()
    {

    }
    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0) && readyToShoot)
        {
            readyToShoot = false;
            RaycastHit hit;
            if (Physics.Raycast(myCameraEyes.position, myCameraEyes.forward, out hit, 100f))
            {
                if (hit.collider.tag.Equals("Enemy"))
                {
                    hit.collider.GetComponent<EnemyHealthSystem>().TakeDamage(gunDamage);
                }
                firingPosition.LookAt(hit.point);
            }
            else
            {
                Vector3 targetPosition = myCameraEyes.position + myCameraEyes.forward * 50f;
                firingPosition.LookAt(targetPosition);
            }
            Instantiate(projectile, firingPosition.position, firingPosition.rotation);
            Instantiate(muzzleFlash, firingPosition.position, firingPosition.rotation);
            StartCoroutine(ResetShot());
        }
    }

    IEnumerator ResetShot()
    {
        yield return new WaitForSeconds(timeBetweenShot);
        readyToShoot = true;
    }
}
