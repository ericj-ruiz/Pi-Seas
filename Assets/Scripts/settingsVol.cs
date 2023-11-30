using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsVol : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    void Start()
    {
        // Set the slider's value to 1
        volumeSlider.value = 1;

        //set slider volume
        AudioListener.volume = volumeSlider.value;
    }




    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
