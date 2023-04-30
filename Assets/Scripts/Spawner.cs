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
    private static UIController myUI;

    void Start()
    {
        myUI = FindObjectOfType<UIController>();
        myUI.waveNumber.SetText("Vague 0");
        myUI.monsterRemaining.SetText("Monstres restant: 0");
    }
    void Update()
    {
        Debug.Log(activeMonsters);
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
            myUI.waveNumber.SetText("Vague " + currentWaveIndex);
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
            myUI.monsterRemaining.SetText("Monstres restant: " + activeMonsters);
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
        Debug.Log("yep");
        activeMonsters--;
        myUI.monsterRemaining.SetText("Monstres restant: " + activeMonsters);
        if (activeMonsters == 0 && currentWaveIndex == waves.Length)
        {
            myUI.win.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}









