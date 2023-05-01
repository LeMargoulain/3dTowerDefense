using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDivision : MonoBehaviour
{
    public GameObject bat;
    public GameObject ghost;
    public void SpawnAfterDeath()
    {
        Instantiate(bat, transform.position, transform.rotation);
        Instantiate(ghost, transform.position, transform.rotation);
        GameManager.RemoveMoney(10);
    }
}
