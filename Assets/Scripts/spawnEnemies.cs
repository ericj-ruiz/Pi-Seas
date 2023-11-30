using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class spawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    //time between enemey spawn
    public float spawnInterval = 1f;

    //display KILLS UI and keeps track of how many
    public TextMeshProUGUI kills;
    public static int displayKills = 0;

    private Vector3 screenBounds;
    //flag to control sound playback
    private bool soundPlay = false;

    [SerializeField] private AudioSource explosionSoundEffect;

    //safe spawning distance
    public float safeDistance = 2.0f; 
    // maximum number of attempts to spawn an enemy safely per frame
    public int maxSpawnAttempts = 3; 

    void Start()
    {
        //calculate screen bounds
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        // start spawning enemies
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
        
        //initial update of kills
        UpdateKillsText(); 
        displayKills = enemy.enemiesKilled;
    }

    void Update()
    {
        //update kills if number of kills has changed
        if(enemy.enemiesKilled != displayKills)
        {
            UpdateKillsText();
            displayKills = enemy.enemiesKilled;
        }
    }

    void SpawnEnemy()
    {
        //attempt to spawn enemy in safe location
        bool spawned = false;
        int attempts = 0;

        while (!spawned && attempts < maxSpawnAttempts)
        {
            //calculate random spawn location
            Vector3 spawnOffset = new Vector3(0f, Random.Range(-screenBounds.y, screenBounds.y), 0f);
            Vector3 spawnPosition = transform.position + spawnOffset;
            //if position safe to spawn intantiate prefab
            if (IsPositionSafe(spawnPosition))
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                spawned = true;
            }
            else
            {
                attempts++;
            }
        }
    }

    private bool IsPositionSafe(Vector3 position)
    {
        //checking if position is too close to other enemies
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("enemy"))
        {
            if ((position - enemy.transform.position).sqrMagnitude < safeDistance * safeDistance)
            {
                //too close
                return false; 
            }
        }
        //position safe
        return true;
    }

    private void UpdateKillsText()
    {
        //update the UI text plays explosion sound
        if (kills != null)
        {
            kills.text = enemy.enemiesKilled.ToString();
            if(soundPlay == true)  
                explosionSoundEffect.Play();
            soundPlay = true;
        }
    }
}