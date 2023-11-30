using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class bossLevel : MonoBehaviour
{
    public GameObject boss; 
    public TextMeshProUGUI bossHpText; //UI boss health
    private float timer = 0f; //timer to track time until boss appears
    public int bossHealth;
    public float timeToAppear = 21f; 
    private bool bossSpawned = false;

    void Start()
    {
        //initialize boss health from bossMovement script the health text on UI
        bossHealth = bossMovement.bossHP;
        bossHpText.text = bossHealth.ToString();
    }

    void Update()
    {
        timer += Time.deltaTime; 

        //check if the timer to spawn the boss and if the boss hasn't already been spawned
        if (timer >= timeToAppear && !bossSpawned)
        {
            //spawn the boss at the current position and rotation of this GameObject
            Instantiate(boss, transform.position, transform.rotation);
            bossSpawned = true; 
        }

        //update the hp per frame 
        bossHealth = bossMovement.bossHP;
        bossHpText.text = bossHealth.ToString();

        //check boss hp is 0
        if (bossHealth <= 0)
        {
            youWin(); 
        }
    }

    public void youWin()
    {
        SceneManager.LoadScene("YOU WIN");
    }
}