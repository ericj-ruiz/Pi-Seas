using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyEnemySpawn : MonoBehaviour
{
    private float timer = 0f;
    public float timeToDestroy = 20f; //time in seconds after which the object will be destroyed 
    
    void Update()
    {
        timer += Time.deltaTime;

        //check if the timer has reached the time to destroy
        if (timer >= timeToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
