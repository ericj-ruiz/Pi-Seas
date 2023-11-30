using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttack : MonoBehaviour
{
    public GameObject bossShotPrefab; //attck prefab 
    public float attackRate = .1f; //attack rate in seconds
    private float timer = 0.0f; //timer to keep track of when to to attack
    private float timeAttack = 0.0f; //timer for attack to appear when boss appears
    public Transform playerTransform; //player's transform for targeting
    [SerializeField] private AudioSource bossAttackSoundEffect; //boss's attack sound effect

    void Update()
    {
        timer += Time.deltaTime; //increment the timer
        timeAttack += Time.deltaTime; 

        //check to see if its okat to attack
        if (timer >= attackRate && timeAttack > 21f)
        {
            ShootShot(); //call function to shoot
            bossAttackSoundEffect.Play(); 
            timer = 0f; //reset the timer for the next attack
        }
    }

    void ShootShot()
    {
        if (playerTransform == null) return;

        //instantiate the projectile at the boss's position with no rotation
        GameObject projectile = Instantiate(bossShotPrefab, transform.position, Quaternion.identity);   

        //calculate the direction vector pointing from the boss to the player
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        //get the bossShot script component from the instantiated projectile
        bossShot bossShotScript = projectile.GetComponent<bossShot>(); 
        {
            //Set the direction to attack move towards the player
            bossShotScript.SetDirection(direction);
        }
    }
}
