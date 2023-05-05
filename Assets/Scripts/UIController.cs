using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI hpText;
    public GameObject panel;
    public GameObject welcomeMessage;
    private bool help = false;
    public TextMeshProUGUI waveNumber;
    public TextMeshProUGUI monsterRemaining;
    public GameObject win;

    public TextMeshProUGUI time;

    private int second;
    private int minute;
    void Start()
    {
        Debug.Log(help);
    }

    // Update is called once per frame
    void Update()
    {
        second = (int)Time.time % 60;
        minute = (int)(Time.time / 60) % 60;
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (help)
            {
                welcomeMessage.SetActive(false);
                help = false;
            }
            else
            {
                welcomeMessage.SetActive(true);
                help = true;
            }
        }

        time.SetText("Temps en jeu: " + minute + ":" + second);


    }

    public void PlayGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Debug.Log("quitter");
        Application.Quit();
    }

}
