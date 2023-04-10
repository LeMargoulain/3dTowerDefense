using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject[] monsters;
    public float spawnInterval;
    public int monsterCount;

    public Wave(GameObject[] monsters, float spawnInterval, int monsterCount)
    {
        this.monsters = monsters;
        this.spawnInterval = spawnInterval;
        this.monsterCount = monsters.Length;
    }


}