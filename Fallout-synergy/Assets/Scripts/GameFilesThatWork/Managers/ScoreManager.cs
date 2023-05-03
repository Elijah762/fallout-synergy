using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public AudioSource missSFX;
    public TMP_Text scoreText;
    static int comboScore;
    void Start()
    {
        Instance = this;
        comboScore = 0;
    }
    public static void Hit()
    {
        comboScore += 1;
        Instance.hitSFX.Play();
    }
    public static void Miss()
    {
        Instance.missSFX.Play();    
    }
    private void Update()
    {
        scoreText.text = "Score: " + comboScore.ToString();
    }
}