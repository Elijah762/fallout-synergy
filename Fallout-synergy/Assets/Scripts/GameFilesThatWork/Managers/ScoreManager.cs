using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public AudioSource hitSFX;
    public AudioSource missSFX;
    public TMP_Text scoreText;
    public TMP_Text multiText;
    public int comboScore;

    private  int currentMulti;
    private  int multiTracker;
    public int[] multiThresh;
    private  int[] stThresh;
    private  int arrLeng;
    
    public  float totalNotes;
    public  float normalHits;
    public  float goodHits;
    public  float perfectHits;
    public  float missHits;

    void Start()
    {
        Instance = this;
        comboScore = 0;
        currentMulti = 1;
        arrLeng = multiThresh.Length;
        stThresh = multiThresh;
    }
    public  void Hit(int score, string type)
    {
        if (type == "normal") normalHits++;
        if (type == "good") goodHits++;
        if (type == "perfect") perfectHits++;
        if (currentMulti - 1 < arrLeng)
        {
            multiTracker += 1;
            if (stThresh[currentMulti - 1] <= multiTracker)
            {
                multiTracker = 0;
                currentMulti++;
            }
        }

        comboScore += score * currentMulti;
        Instance.hitSFX.Play();
    }
    public  void Miss()
    {
        multiTracker = 0;
        currentMulti = 1;
        missHits++;
        Instance.missSFX.Play();    
    }
    private void Update()
    {
        scoreText.text = "Score: " + comboScore;
        multiText.text = "Multiplier: " + currentMulti + "x";
    }

    public string getRank(float percent)
    {
        if (percent > 95)
        {
            return "S";
        }

        if (percent > 90)
        {
            return "A";
        }

        if (percent > 80)
        {
            return "B";
        }
        if (percent > 70)
        {
            return "D";
        }

        return "F";
    }
}