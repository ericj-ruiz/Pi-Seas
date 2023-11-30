using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class enemy : MonoBehaviour
{

    public float speed = 5.0f;

   
    private Rigidbody2D rb;
    private Vector3 screenBounds;
    private float objectWidth; 
    private float objectHeight;


    public static int enemiesKilled; //kill count of enemies

    public GameObject explosionPrefab;
    private bool isHit = false; //flag to kill enemey end only add 1 kill to kill count
    


    // Use this for initialization
    void Start () {
        //moves enemy from right to left
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
       




        //sets the screen bounds
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //half the width/heigh of the object
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; 
        
    }


   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject) 
        {
            //check if enemy has been hit
            if(!isHit)
            {
                isHit = true;
                enemiesKilled++;
            }
            //instantiate explosion effect
            if (explosionPrefab)
            {
        
                GameObject explosionEffect = Instantiate(explosionPrefab, transform.position, transform.rotation);
                //destroy the explosion effect after its duration
                Destroy(explosionEffect, explosionEffect.GetComponent<ParticleSystem>().main.duration);
            }
        
            Destroy(gameObject);
            
            
        }
    }
    void Update () {

          //DESTROY OBJECT ONCE OUT OF BOUNDS
         if (transform.position.x < screenBounds.x * -1 - objectWidth || 
            transform.position.x > screenBounds.x + objectWidth || 
            transform.position.y < screenBounds.y * -1 - objectHeight || 
            transform.position.y > screenBounds.y + objectHeight)
        {
        
            Destroy(gameObject);
        
            
        }

       

    }
   
}
