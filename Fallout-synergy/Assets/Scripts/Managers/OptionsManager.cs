using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog;

    public List<ResItem> resolutions = new List<ResItem>();

    private int selectedResolution;

    public TMP_Text resolutionLabel;
    // Start is called before the first frame update
    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        vsyncTog.isOn = QualitySettings.vSyncCount != 0;

        bool foundResolution = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundResolution = true;

                selectedResolution = i;
                
                UpdateResLabel();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResLeft()
    {
        selectedResolution--;
        if (selectedResolution < 0) {selectedResolution = 0;}
        
        UpdateResLabel();
    }

    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count - 1) {selectedResolution = resolutions.Count - 1;}
        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();
    }
    
    public void ApplyGraphics()
    {
        QualitySettings.vSyncCount = vsyncTog ? 1 : 0;
        
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenTog.isOn);
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}