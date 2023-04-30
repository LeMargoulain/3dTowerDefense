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
    void Start()
    {
        Debug.Log(help);
    }

    // Update is called once per frame
    void Update()
    {
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
