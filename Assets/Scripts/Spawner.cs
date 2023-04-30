using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawn;
    public Wave[] waves;
    private int activeMonsters = 0;
    private int currentWaveIndex = 0;
    private bool waitingForInput = true;

    void Update()
    {
        Debug.Log(waitingForInput);
        if (waitingForInput && Input.GetKeyDown(KeyCode.R))
        {
            waitingForInput = false;
            StartNextWave();
        }
    }

    private void StartNextWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            Wave wave = waves[currentWaveIndex];
            currentWaveIndex++;

            StartCoroutine(SpawnWave(wave));
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        yield return new WaitUntil(() => !waitingForInput);

        for (int i = 0; i < wave.monsters.Length; i++)
        {
            Instantiate(wave.monsters[i], spawn.position, spawn.rotation);
            activeMonsters++;
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        waitingForInput = true;
        while (activeMonsters > 0)
        {
            yield return null;
        }
    }


    public void OnMonsterDestroyed()
    {
        activeMonsters--;
    }
}









