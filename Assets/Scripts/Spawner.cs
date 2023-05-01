using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawn;
    public Wave[] waves;
    private int currentWaveIndex = 0;
    private int currentMonsterIndex = 0;
    private bool waitingForInput = true;
    private int remainingMonsters = 0;
    private static UIController myUI;

    void Start()
    {
        myUI = FindObjectOfType<UIController>();
        myUI.waveNumber.SetText("Vague 0/" + waves.Length);
        myUI.monsterRemaining.SetText("Monstres restant:" + GameManager.GetMonsterNumber());
    }
    void Update()
    {
        myUI.monsterRemaining.SetText("Monstres restant: " + remainingMonsters);
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
            remainingMonsters = waves[currentWaveIndex].MonsterNumberInWave();
            currentWaveIndex++;
            myUI.waveNumber.SetText("Vague " + currentWaveIndex + "/" + waves.Length);
            currentMonsterIndex = 0;
            StartCoroutine(SpawnWave(wave));
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        yield return new WaitUntil(() => !waitingForInput);
        for (int i = 0; i < wave.monsters.Length; i++)
        {
            currentMonsterIndex++;
            Instantiate(wave.monsters[i], spawn.position, spawn.rotation);
            //myUI.monsterRemaining.SetText("Monstres restant: " + GameObject.FindGameObjectsWithTag("Enemy").Length);
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        waitingForInput = true;
        while (GameManager.GetMonsterNumber() > 0)
        {
            yield return null;
        }

    }


    public void OnMonsterDestroyed()
    {
        Debug.Log(GameManager.GetMonsterNumber());
        if (remainingMonsters > 0) remainingMonsters--;
        if (currentWaveIndex == waves.Length && remainingMonsters == 0 && GameManager.GetMonsterNumber() - 1 <= 0)
        {
            myUI.win.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }

}









