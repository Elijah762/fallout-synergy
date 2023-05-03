using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public string firstLevel;
    public static StartManager Instance;

    public GameObject optionsScreen;

    private void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quiting");
    }
}
