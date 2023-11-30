using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mathSelection : MonoBehaviour
{
    //UI buttons for each operator
    public Button addButton;
    public Button subButton;
    public Button multButton;
    public Button divButton;
    public Button randButton;
    
    //static variable so it can pass between scenes
    //-1 being the null value 
    public static int selectOperator =-1;

    void Start()
    {
        //which ever is clicked becomes the operator used and operator value assigned
        addButton.onClick.AddListener(() => selectOperator =0);
        subButton.onClick.AddListener(() => selectOperator =1);
        multButton.onClick.AddListener(() => selectOperator =2);
        divButton.onClick.AddListener(() => selectOperator =3);
        randButton.onClick.AddListener(() => selectOperator =4);
        

    }
    public void next(){
    
        SceneManager.LoadScene("MathGenerator");
        
    }
}
