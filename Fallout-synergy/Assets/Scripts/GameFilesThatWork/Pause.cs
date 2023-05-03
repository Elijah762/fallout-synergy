using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pause : MonoBehaviour
{
    public GameObject paused;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                SongManager.Instance.PauseSong();
                Lane.Instance.canUpdate = false;
                paused.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                SongManager.Instance.StartSong();
                Lane.Instance.canUpdate = true;
                paused.SetActive(false);
            }
        }
    }
}