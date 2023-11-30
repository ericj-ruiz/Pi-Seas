using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class timer2 : MonoBehaviour
{
    public TextMeshProUGUI timeText;
void Update()
    {
        //receiving the time from first timer which acts as a background time
        //and continuing it in this script
        float currentTime = timer.Instance.t;
        string minutes = ((int)currentTime / 60).ToString();
        string seconds = (currentTime % 60).ToString("f2");
        //updating UI time
        timeText.text = minutes + ":" + seconds;
        
    }
}
