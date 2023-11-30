using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
  //player movement direction
  private Vector3 direction; 

  //special speed jump ability UI text
  public TextMeshProUGUI specialStext; 
  //special speed jump ability
  public int specialSpeedUses = 10;

  //health ability
  public int hp = 10;
  public TextMeshProUGUI hpText;
  
  //physics properties
  public float gravity = -9.8f;
  public float strength = 5.5f;

  //audio sources
  [SerializeField] private AudioSource jumpSoundEffect;
  [SerializeField] private AudioSource hpHitSoundEffect;
  [SerializeField] private AudioSource specialSpeedSoundEffect;

 private void Start()
 {
    //adjust abilities if in Boss level
    //adjus by amount correct and operator selected
    Scene activeScene = SceneManager.GetActiveScene();
    if (activeScene.name == "BOSS")
    {
      //apply value to abilities
      hp+= mathAbilityManager.add;
      specialSpeedUses += mathAbilityManager.sub;
    }

 }
 
  private void Update()
  {
    //get the updated value for UI text
    specialStext.text = specialSpeedUses.ToString();
    hpText.text = hp.ToString();

    //space bar for normal jump 
    if (Input.GetKeyDown(KeyCode.Space))
    {
      direction = Vector3.up * strength;
      jumpSoundEffect.Play();
    }

    //special speed bounce key W
    if (Input.GetKeyDown(KeyCode.W) && specialSpeedUses > 0)
    {
      direction = Vector3.up * (strength +5);
      specialSpeedUses--;
      specialSpeedSoundEffect.Play();
    }

    //apply gravity and update the player position
    direction.y += gravity * Time.deltaTime;
    transform.position += direction * Time.deltaTime;

  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    //if collide with enemy attack decrease -1 hp
    if(collision.gameObject)
    {
      hp--;
      hpHitSoundEffect.Play();
      
      //if player hp 0 send to game over scene
      if(hp <=0)
      {
        gameOver();
      }
    }
  }

  public void gameOver()
  {
    SceneManager.LoadScene("GAME OVER");
  }


}
