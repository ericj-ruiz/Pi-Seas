using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    //reference to meshRenderer
    private MeshRenderer meshRenderer;
    //speed of parrallax effect
    public float animationSpeed = 0.1f;


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        //adjust the texture off set by animation speed creating the parallax effect
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }
}
