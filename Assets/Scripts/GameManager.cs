using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int money = 100;
    private static int hp = 5;
    private static UIController myUI;
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        money = 100;
        hp = 10;
        myUI = FindObjectOfType<UIController>();
        myUI.moneyText.SetText(money.ToString());
        myUI.hpText.SetText(hp.ToString());
        myUI.panel.SetActive(false);
        myUI.win.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void AddMoney(int moneyToAdd)
    {
        money += moneyToAdd;
        myUI.moneyText.SetText(money.ToString());
    }

    public static int getMoney()
    {
        return money;
    }

    public static void RemoveMoney(int moneyToRemove)
    {
        money -= moneyToRemove;
        myUI.moneyText.SetText(money.ToString());
    }

    public static void DamagePlayer()
    {
        hp--;
        myUI.hpText.SetText(hp.ToString());
        if (hp <= 0)
        {
            myUI.panel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public static int GetMonsterNumber()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}
