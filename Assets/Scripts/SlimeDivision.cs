using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDivision : MonoBehaviour
{
    public GameObject bat;
    public GameObject ghost;
    public void SpawnAfterDeath()
    {
        Debug.Log("J'ai été détruit");
        Instantiate(bat, transform.position, transform.rotation);
        Instantiate(ghost, transform.position, transform.rotation);
        Spawner.AddMonster();
        Spawner.AddMonster();
    }
}
