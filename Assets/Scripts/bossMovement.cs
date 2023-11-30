using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f; 
    private Camera mainCamera; 
    private float topBoundary; //boundary for boss movement
    private float bottomBoundary;
    private float fixedXPosition; //X position for the boss
    [SerializeField] private AudioSource bossHealthSoundEffect;

    public static int bossHP = 70; //to pass to different scripts boss health points

    void Start()
    {
        mainCamera = Camera.main; 
        fixedXPosition = transform.position.x; //store the initial X position of the boss
        CalculateCameraBoundaries();
        StartCoroutine(MoveUpDown()); //start the coroutine to move the boss up and down
    }

    IEnumerator MoveUpDown()
    {
        while (true) //infinite loop to keep the boss moving
        {
            //move the boss from bottom to top
            yield return MoveBetweenPoints(bottomBoundary, topBoundary);
            //move the boss from top to bottom
            yield return MoveBetweenPoints(topBoundary, bottomBoundary);
        }
    }

    IEnumerator MoveBetweenPoints(float start, float end)
    {
        float timer = 0;
        while (timer < 1f) //move over time to create a smooth transition
        {
            float newY = Mathf.Lerp(start, end, timer); //interpolate between the start and end points
            transform.position = new Vector3(fixedXPosition, newY, transform.position.z); //update y position
            timer += Time.deltaTime * moveSpeed; //increment timer based on move speed
            yield return null; //wait for the next frame
        }
    }

    void CalculateCameraBoundaries()
    {
        //calculate verticle boundaries based on camera
        float cameraHeight = 2f * mainCamera.orthographicSize;
        topBoundary = mainCamera.transform.position.y + cameraHeight / 2f - transform.localScale.y / 2f;
        bottomBoundary = mainCamera.transform.position.y - cameraHeight / 2f + transform.localScale.y / 2f;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if collsion is not its own attack
        if(!collision.gameObject.CompareTag("bossShots"))
        {
            bossHP--; 
            bossHealthSoundEffect.Play(); 
        }
    }
}
