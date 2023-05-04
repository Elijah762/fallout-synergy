using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public KeyCode input;
    public GameObject notePrefab;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();
    public static Lane Instance;

    public Sprite defKeyUp;
    public Sprite defKeyDown;
    public bool canUpdate = true;

    int spawnIndex = 0;
    int inputIndex = 0;

    public GameObject normal;
    public GameObject good;
    public GameObject perfect;
    public GameObject miss;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (canUpdate)
        {
            if (spawnIndex < timeStamps.Count)
            {
                if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
                {
                    var note = Instantiate(notePrefab, transform);
                    note.SetActive(true);
                    notes.Add(note.GetComponent<Note>());
                    note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                    spawnIndex++;
                }
            }

            if (inputIndex < timeStamps.Count)
            {
                double timeStamp = timeStamps[inputIndex];
                double marginOfError = SongManager.Instance.marginOfError;
                double audioTime = SongManager.GetAudioSourceTime() -
                                   (SongManager.Instance.inputDelayInMilliseconds / 1000.0);

                if (Input.GetKeyDown(input))
                {

                    if (Math.Abs(audioTime - timeStamp) < marginOfError)
                    {
                        GoodHit();
                        Destroy(notes[inputIndex].gameObject);
                        inputIndex++;
                    }
                    else if (Math.Abs(audioTime - timeStamp) < marginOfError * .75)
                    {
                        GreatHit();
                        Destroy(notes[inputIndex].gameObject);
                        inputIndex++;
                    }
                    else if(Math.Abs(audioTime - timeStamp) < marginOfError * .25)
                    {
                        PerfHit();
                        Destroy(notes[inputIndex].gameObject);
                        inputIndex++;
                    }
                }

                if (timeStamp + marginOfError <= audioTime)
                {
                    Miss();
                    inputIndex++;
                }
            }
        }
    }
    private void GoodHit()
    {
        //Instantiate(normal, normal.transform.position, normal.transform.rotation);
        ScoreManager.Instance.Hit(1, "normal");
    }
    private void GreatHit()
    {
        //Instantiate(good, good.transform.position, good.transform.rotation);
        ScoreManager.Instance.Hit(3, "good");
    }
    private void PerfHit()
    {
        //Instantiate(perfect, perfect.transform.position, perfect.transform.rotation);
        ScoreManager.Instance.Hit(6, "perfect");
    }
    private void Miss()
    {
        //Instantiate(miss, miss.transform.position, miss.transform.rotation);
        ScoreManager.Instance.Miss();
    }
}