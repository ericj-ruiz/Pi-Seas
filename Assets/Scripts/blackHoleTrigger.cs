using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class blackHoleTrigger : MonoBehaviour
{
    public float levelChangeTime = 75f; //black hole time
    public float speed = 5.5f; //speed of the black hole 
    public bool isActive = false; 
    public TextMeshProUGUI bhActive; 
    public float rotationSpeed = 11.0f; 


    private float timerForBH = 0f; 
    private GameObject player; 
    private shieldProtection shieldPro; //reference to the shieldProtection script
    private bool shieldWasActive = false; 

    private void Start()
    {
        //find and set references at the start
        player = GameObject.FindGameObjectWithTag("Hero");
        shieldPro = FindObjectOfType<shieldProtection>();
    }

    private void Update()
    {
        //check if BH active
        if (!isActive)
        {
            //if shield is present and active
            if (shieldPro != null && shieldPro.isShieldActive)
            {
                //check if the shield has just been activated
                if (!shieldWasActive) 
                {
                    levelChangeTime += .1f; //add extra time
                    shieldWasActive = true; //set the flag to true
                }
            }
            else
            {
                shieldWasActive = false; //reset the flag when the shield is not active
            }

            //update the timer
            timerForBH += Time.deltaTime;

            //check if timer has reached the level change time
            if (timerForBH >= levelChangeTime)
            {
                isActive = true;
                bhActive.text = "Warning"; //show player black hole coming 
                bhActive.color = Color.red; //change UI text color
            }
        }
        else
        {
            //when black hole is active
            if(player != null)
            {
                //calculate the direction towards the player
                Vector3 direction = (player.transform.position - transform.position).normalized;
                //move the black hole towards the player
                transform.Translate(direction * speed * Time.deltaTime);

                //rotate the black hole
                transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            }
        }
        
        checkCollision();
    }

    private void checkCollision()
    {
        //check for collisions within a radius around the black hole
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3.5f); 

        foreach (Collider2D collide in colliders)
        {
            if (collide.CompareTag("Player"))
            {
                SceneManager.LoadScene("MathSelection");
            }
        }
    }
}

