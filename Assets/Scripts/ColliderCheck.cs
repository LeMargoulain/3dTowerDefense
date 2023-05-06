using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheck : MonoBehaviour
{
    private Collider mycollider;
    private bool wellPlaced = false;
    void Start()
    {
        mycollider = GetComponent<Collider>();
        StartCoroutine(Placement());

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Tower")
        {
            if (!wellPlaced)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    IEnumerator Placement()
    {
        yield return new WaitForSeconds(0.1f);
        wellPlaced = true;
    }



}
