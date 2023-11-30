using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class dataBaseManager : MonoBehaviour
{
  [SerializeField]
    private string postURL = "https://nrs-projects.humboldt.edu/~ejr66/pi-seas/insertWinner.php"; 

    public void InsertWinnerIntoDatabase()
    {
        //accessing static variables directly from the winner class
        StartCoroutine(PostWinnerToDatabase(winner.initials, winner.numOfCorrectScore, winner.timeOfWin, winner.numOfKills, winner.date));
    }

    //coroutine to post winner data to database
    private IEnumerator PostWinnerToDatabase(string initials, int score, float time, int kills, string date)
    {
        //creating form send data through POST method
        WWWForm form = new WWWForm();
        form.AddField("initials", initials);
        form.AddField("score", score.ToString());
        form.AddField("time", time.ToString());
        form.AddField("kills", kills.ToString());
        form.AddField("date", date);

        //sending form to the server
        using (UnityWebRequest www = UnityWebRequest.Post(postURL, form))
        {
            yield return www.SendWebRequest(); //waiting until sent and response received

        }
    }
}