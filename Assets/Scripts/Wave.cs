using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] monsters;
    public float spawnInterval;
    private int monsterInWave = 0;

    void Start()
    {

        Debug.Log(monsterInWave);
    }

    public Wave(GameObject[] monsters, float spawnInterval, int monsterCount)
    {
        this.monsters = monsters;
        this.spawnInterval = spawnInterval;
    }

    public int MonsterNumberInWave()
    {
        foreach (GameObject monster in monsters)
        {
            if (monster.GetComponent<SlimeDivision>() != null)
            {
                monsterInWave += 3;
            }
            else monsterInWave += 1;
        }
        return monsterInWave;
    }

}