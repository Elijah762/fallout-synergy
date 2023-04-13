using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog;

    public List<ResItem> resolutions = new List<ResItem>();

    private int _selectedResolution;

    public TMP_Text resolutionLabel;

    public AudioMixer theMixer;

    public TMP_Text masterLabel, musicLabel, sfxLabel;

    public Slider masterSlider, musicSlider, sfxSlider;
    // Start is called before the first frame update
    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        vsyncTog.isOn = QualitySettings.vSyncCount != 0;

        var foundResolution = false;
        for (var i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundResolution = true;
                _selectedResolution = i;
                UpdateResLabel();
            }
        }

        if (!foundResolution)
        {
            var newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;
            
            resolutions.Add(newRes);
            _selectedResolution = resolutions.Count - 1;
            UpdateResLabel();
        }
        
        theMixer.GetFloat("MasterVol", out var maVol);
        masterSlider.value = maVol;
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        
        theMixer.GetFloat("MusicVol", out var muVol);
        musicSlider.value = muVol;
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        theMixer.GetFloat("SFXVol", out var sVol);
        sfxSlider.value = sVol;
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResLeft()
    {
        _selectedResolution--;
        if (_selectedResolution < 0) {_selectedResolution = 0;}
        
        UpdateResLabel();
    }

    public void ResRight()
    {
        _selectedResolution++;
        if (_selectedResolution > resolutions.Count - 1) {_selectedResolution = resolutions.Count - 1;}
        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[_selectedResolution].horizontal.ToString() + " x " + resolutions[_selectedResolution].vertical.ToString();
    }
    
    public void ApplyGraphics()
    {
        QualitySettings.vSyncCount = vsyncTog ? 1 : 0;
        
        Screen.SetResolution(resolutions[_selectedResolution].horizontal, resolutions[_selectedResolution].vertical, fullscreenTog.isOn);
    }

    public void SetMasterVolume()
    {
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();

        theMixer.SetFloat("MasterVol", masterSlider.value);
        
        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    }
    
    public void SetMusicVolume()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        theMixer.SetFloat("MusicVol", musicSlider.value);
        
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }
    
    
    public void SetSFXVolume()
    {
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();

        theMixer.SetFloat("SFXVol", sfxSlider.value);
        
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
