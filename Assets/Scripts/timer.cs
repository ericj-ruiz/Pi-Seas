using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class timer : MonoBehaviour
{
    //intance of a timer
    public static timer Instance;
    //UI text of time
    public TextMeshProUGUI timeText;
    //start time
    private float startTime;
    //time passed since start of time
    public float t;
    
    private void Awake()
    {
        //ensures only one instance of timer exists
        if (Instance == null)
        {
            Instance = this;
              
            // creates a new root GameObject and move the script to it and does not destroy
           GameObject root = new GameObject("TimerRoot");
           Canvas mainCanvas = FindObjectOfType<Canvas>();
            if (mainCanvas != null)
            {
                //setting main canvas as a child of the new root object
                 mainCanvas.transform.SetParent(root.transform);
            }
            //preventing from destroying
            DontDestroyOnLoad(root);
            //recording the start time of timer
            startTime = Time.time;

        }
        else
        {   //if instance exists destroy the new one
            Destroy(gameObject);
            return;
        }
        

        
    }

    
    void Update()
    {
        //calculating the elapsed time
        t = Time.time - startTime;

        //converting to minutes and seconds
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        //updating UI text
        timeText.text = minutes + ":" + seconds;
        
    }
}