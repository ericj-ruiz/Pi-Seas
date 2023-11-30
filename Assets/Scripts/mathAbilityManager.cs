using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mathAbilityManager : MonoBehaviour
{
    
    public static int add=0; //HP abillity
    public static int sub=0; //speed jump
    public static int mult=0; //shield
    public static int div=0; //super shot
    public static int rand=0; //machine gun

    void Start()
    {
        //getting the operator selected
        int operation = mathSelection.selectOperator;
        Scene activeScene = SceneManager.GetActiveScene();

        //check if the boss scene is being used to add the ability 
        if (activeScene.name == "BOSS")
        {
            //checking to see if the operator was chosen
            //if it wasnt chosen the ability variable will have the value zero
            if(operation != -1)
            {
                //receiving the score from the math generator
                int correctP = mathPs.correctPoints;
                //adding the score to whichever chosen  
                if (operation == 0) // Addition
                {
                    add = correctP;
                }
                else if (operation == 1) // Subtraction
                {
                    sub = correctP;
                }
                else if (operation == 2) // Multiplication
                {
                    mult = correctP;
                }
                else if (operation == 3) // Division
                {
                    div = correctP;
                }
                else if (operation == 4) // Random
                {
                    rand = correctP;
                }
                
            }
            
        }
    }
    
}
