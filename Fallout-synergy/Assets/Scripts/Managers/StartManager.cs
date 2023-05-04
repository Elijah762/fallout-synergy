using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public string firstLevel;
    public static StartManager Instance;
    public static string SelectedSong;
    public static float difficulty;
    
    public GameObject optionsScreen;
    public GameObject selectScreen;
    public GameObject difficultyScreen;

    private void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        difficultyScreen.SetActive(true);
    }

    public void SelectScreen(float diff)
    {
        difficulty = diff;
        selectScreen.SetActive(true);
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

    public void SetMusicAfrica(string music)
    {
        SelectedSong = music;
        SceneManager.LoadScene("GameAfrica");
    }
    public void SetMusicMambo(string music)
    {
        SelectedSong = music;
        SceneManager.LoadScene("GameMombo");
    }
    public void SetMusicOther(string music)
    {
        SelectedSong = music;
        SceneManager.LoadScene("GameSunrise");
    }

    public void CursedButton()
    {
        SceneManager.LoadScene("Gridmap");
    }
}
