using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class winner : MonoBehaviour
{
    //static variables to receive, display and send to other scripts
    public static int numOfKills;
    public static float timeOfWin;
    public static int numOfCorrectScore;
    public static string date;
    public static string initials;

    //UI text fields
    public TextMeshProUGUI timeOfWinText;
    public TextMeshProUGUI numOfKillsText;
    public TextMeshProUGUI numOfCorrectScoreText;
    public TextMeshProUGUI dateText;

    //UI input initials field
    public TMP_InputField initialsInputField;

    // Start is called before the first frame update
    void Start()
    {
        // receiving game data from other scripts
        timeOfWin = timer.Instance.t;
        numOfCorrectScore = mathPs.correctPoints;
        numOfKills = enemy.enemiesKilled;

        //UI text display of data
        numOfCorrectScoreText.text = numOfCorrectScore.ToString();
        numOfKillsText.text = numOfKills.ToString();
        //calculate the time in min and secs
        string minutes = ((int)timeOfWin / 60).ToString();
        string seconds = (timeOfWin % 60).ToString("f2");
        timeOfWinText.text = minutes + ":" + seconds;

        //getting date and displaying the UI text
        DateTime today = DateTime.Today;
        date = today.ToString("yyy-MM-dd");
        dateText.text = date;

        // getting player initials from input
        if (initialsInputField != null)
        {
            initials = initialsInputField.text;
        }
    }

    // call this method to update and save initials
    public void SubmitInitials()
    {
        if (initialsInputField != null)
        {
            initials = initialsInputField.text; 
        }
    }

}