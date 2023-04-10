using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawn;
    public Wave[] waves;
    public float timeBetweenWaves;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.monsterCount; i++)
        {
            Instantiate(wave.monsters[i], spawn.position, spawn.rotation);
            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }

    private IEnumerator SpawnWaves()
    {
        //yield return new WaitForSeconds(timeBeforeFirstWave);

        for (int i = 0; i < waves.Length; i++)
        {
            yield return StartCoroutine(SpawnWave(waves[i]));
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}
