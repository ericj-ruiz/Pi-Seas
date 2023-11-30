using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsBrightness : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider;

    private Image backgroundImage;

    void Start()
    {
        //find the background image by tag
        GameObject backgroundObj = GameObject.FindGameObjectWithTag("background");
        if (backgroundObj != null)
        {
            backgroundImage = backgroundObj.GetComponent<Image>();
        }

        //initialize the slider value 
        brightnessSlider.value = backgroundImage != null ? backgroundImage.color.a : 1.0f; 
    }

    public void ChangeBrightness()
    {
        if (backgroundImage != null)
        {
            //interpolate alpha between 0.5 and 1 based on the slider's value to not go completely dark
            float alphaValue = 0.5f + 0.5f * brightnessSlider.value;
            Color newColor = backgroundImage.color;
            newColor.a = alphaValue;
            backgroundImage.color = newColor;
        }
    }
}
