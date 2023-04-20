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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
