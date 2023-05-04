using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.IO;
using UnityEngine.Networking;
using System;
using UnityEditor;
using UnityEngine.UI;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public AudioSource audioSource;
    public Lane[] lanes;
    public float songDelayInSeconds;
    public double marginOfError; // in seconds

    public int inputDelayInMilliseconds;

    public string fileLocation;
    public float noteTime;
    public float noteSpawnY;
    public float noteTapY;
    
    public GameObject results, endButton;
    public Text percentHit, normalText, goodText, perfectText, missText, rankText, finalScoreText;
    public float noteDespawnY
    {
        get
        {
            return noteTapY - (noteSpawnY - noteTapY);
        }
    }

    public static MidiFile midiFile;
    // Start is called before the first frame update
    void Start()
    {
        marginOfError = StartManager.difficulty;
        fileLocation = StartManager.SelectedSong + ".mid";
        
        Instance = this;
        if (Application.streamingAssetsPath.StartsWith("http://") || Application.streamingAssetsPath.StartsWith("https://"))
        {
            StartCoroutine(ReadFromWebsite());
        }
        else
        {
            ReadFromFile();
        }
    }

    private IEnumerator ReadFromWebsite()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Application.streamingAssetsPath + "/" + fileLocation))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                byte[] results = www.downloadHandler.data;
                using (var stream = new MemoryStream(results))
                {
                    midiFile = MidiFile.Read(stream);
                    GetDataFromMidi();
                }
            }
        }
    }

    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + fileLocation);
        GetDataFromMidi();
    }
    public void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);

        foreach (var lane in lanes) lane.SetTimeStamps(array);

        Invoke(nameof(StartSong), songDelayInSeconds);
    }
    public void StartSong()
    {
        audioSource.Play();
    }

    public void PauseSong()
    {
        audioSource.Pause();
    }
    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            results.SetActive(true);
            var perc = (ScoreManager.Instance.totalNotes -
                          ScoreManager.Instance.missHits) / ScoreManager.Instance.totalNotes;
            percentHit.text = perc.ToString("F1") + "%";
            normalText.text = ScoreManager.Instance.normalHits.ToString();
            goodText.text = ScoreManager.Instance.goodHits.ToString();
            perfectText.text = ScoreManager.Instance.perfectHits.ToString();
            missText.text = ScoreManager.Instance.missHits.ToString();
            rankText.text = ScoreManager.Instance.getRank(perc);
            finalScoreText.text = ScoreManager.Instance.comboScore.ToString();
            endButton.SetActive(true);
        }
        else
        {
            results.SetActive(false);
            endButton.SetActive(false);
        }
    }
}
