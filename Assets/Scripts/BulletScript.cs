using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousePos; 
    private Camera mainCam; 
    private Rigidbody2D rb; 
    public float force = 5; 
    private float lifespan = 5f; //time after which the bullet will be destroyed
    private float timer = 0f; //timer to track the lifespan of the bullet

    public bool isSuperShot; //flag to determine if the bullet is a super shot

    private void Start()
    {
        //getting the main camera and Rigidbody2D component
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();

        //calculating the direction towards the mouse position
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); //convert mouse position to world coordinates
        Vector3 direction = mousePos - transform.position; //determine direction towards the mouse position

        //setting the velocity of the bullet
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force; 
    }

    private void Update()
    {
        timer += Time.deltaTime; 
        if (timer > lifespan) //check if the timer exceeds the lifespan to destroy bullet
        {
            Destroy(gameObject); 
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check that bullet is not supershot because it would destroy normal bullet on collision
        if(!isSuperShot)
        {
            Destroy(gameObject);
        }
    }
}