using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class shieldProtection : MonoBehaviour
{
    //number of uses for shields
    public int numOfUses = 10;
    //UI for shield text
    public TextMeshProUGUI usesText;

    public float shieldDuration = 3f;
    //when active green bubble
    public Color activeColor = new Color(0.0f, .35f, 0.0f, 0.50f); 
    //when not white
    public Color defaultColor = new Color(1.0f, 1.0f, 1.0f, 0.50f);

    //flag to track if shield active for BH script
    public bool isShieldActive;
    private SpriteRenderer spriteRenderer; 

    //flag to check if shield can be activated
    private bool isShieldEnabled = true;
    private blackHoleTrigger bhTrigger;

   [SerializeField] private AudioSource shieldSoundEffect;

    
    void Start()
    {
        //add value to ability if in boss scene
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "BOSS")
        {
           numOfUses+= mathAbilityManager.mult; 
        }

        //initialize sprite to default color;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = defaultColor;

        //update UI shield text
        usesText.text = numOfUses.ToString();
        //get bh trigger script component
        bhTrigger = FindObjectOfType<blackHoleTrigger>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //disable shield activation when black hole is active
        if(bhTrigger != null && bhTrigger.isActive)
            isShieldEnabled = false;
        else
            isShieldEnabled = true;
         
        //if Q is pressed and there are still uses 
        // also if bh is not active then shield is enabled 
         if (Input.GetKeyDown(KeyCode.Q) && numOfUses > 0 && !isShieldActive && isShieldEnabled)
        {
            //disable the collider
            GetComponent<Collider2D>().enabled = false;

            //change the sprite color to the active color
            spriteRenderer.color = activeColor;

            //update the ability state and reduce the number of uses.
            isShieldActive = true;
            numOfUses--;
            shieldSoundEffect.Play();
            usesText.text = numOfUses.ToString();

            //reset shield to 3f
            shieldDuration = 3f;
        }

        //handle shield deactivation after duration
        if (isShieldActive)
        {
            //decrease duration overtime
            shieldDuration -= Time.deltaTime;

            if (shieldDuration <= 0)
            {
                //reactivate collider and reset color 
                GetComponent<Collider2D>().enabled = true;
                spriteRenderer.color = defaultColor;

                //update shield is no longe active
                isShieldActive = false;
            }
        }
    }

        
    
}
