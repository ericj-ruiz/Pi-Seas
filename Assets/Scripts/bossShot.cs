using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossShot : MonoBehaviour
{
    public float speed = 11.0f; //speed of shot
    private float lifespan = 3f;  //time after which the shot is destroyed
    private float timer = 0f; //timer to track the lifespan of the shot

    //method to set the direction of shot
    public void SetDirection(Vector2 direction)
    {
        //get rigidbody of object
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //set velocity of the rb to the normalized direction multiplied by speed
        rb.velocity = direction.normalized * speed;
    }  
    
    private void Update()
    {
        timer += Time.deltaTime;
        //check if the timer vs lifespan to destroy shot
        if (timer > lifespan)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if shot has collided with any object
        if(collision.gameObject)
        {
            Destroy(gameObject);
        }
    }
}