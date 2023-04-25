using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100f, bulletLife = 2f;
    public Rigidbody myRigidBody;

    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Destroy(transform.parent.gameObject, bulletLife);
    }


    private void FixedUpdate()
    {
        myRigidBody.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.parent.gameObject);
    }

    /* private IEnumerator DestroyBulletTime()
     {
         yield return new WaitForSeconds(bulletLife);
         Destroy(gameObject);
     }*/

    private void DestroyBulletTime()
    {
        bulletLife -= Time.deltaTime;
        if (bulletLife < 0)
        {
            Destroy(gameObject);
        }
    }
}
