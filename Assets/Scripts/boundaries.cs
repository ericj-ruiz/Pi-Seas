using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundaries : MonoBehaviour 
{
    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start () {
        //calculate the screen boundaries in world space
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        //calculate size of width and height / 2
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; 
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; 
    }

    void LateUpdate(){
        //store current position
        Vector3 viewPos = transform.position;
        //clamp x and y position to keep objects within bounds
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        //update object position
        transform.position = viewPos;
    }
}