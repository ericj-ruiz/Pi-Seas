using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{ 

   public void PlayGame()
   {
    SceneManager.LoadScene("Pi-Seas");
   }
   public void menu()
   {
      SceneManager.LoadScene("Menu Scene");
   }
   public void quit()
   {
         // Quit the game once built
       /* Application.Quit();

        //quit in the editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        */

        #if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalEval("reloadPage()");
        #endif
   }

  
}
