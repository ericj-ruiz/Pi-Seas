using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class displayWinner : MonoBehaviour
{
    //UI to display each stat on the ending leaderboard 
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI initialsText;

    public dataBaseManager dbManager; 
    void Start()
    {
        // recieving time of win in seconds 
        float t = winner.timeOfWin;
        //calculating minutes and seconds of win time
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
 
        //setting UI texts with data
        scoreText.text = winner.numOfCorrectScore.ToString();
        killsText.text = winner.numOfKills.ToString(); 
        timeText.text = minutes + ":" + seconds;
        dateText.text = winner.date;
        initialsText.text = winner.initials;

    }
    //calling function to insert data into the database
    public void SendDataToDatabase()
    {
        dbManager.InsertWinnerIntoDatabase();
    }

}
