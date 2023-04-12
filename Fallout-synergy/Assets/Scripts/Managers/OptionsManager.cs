using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog;
    // Start is called before the first frame update
    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        vsyncTog.isOn = QualitySettings.vSyncCount != 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyGraphics()
    {
        Screen.fullScreen = fullscreenTog.isOn ;

        QualitySettings.vSyncCount = vsyncTog ? 1 : 0;
    }
}
