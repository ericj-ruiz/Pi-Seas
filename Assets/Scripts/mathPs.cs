using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class mathPs : MonoBehaviour
{
    //UI text to display math generator
    public TextMeshProUGUI firstNumber;
    public TextMeshProUGUI secondNumber;
    public TextMeshProUGUI answer1;
    public TextMeshProUGUI answer2;
    public TextMeshProUGUI answer3;
    public TextMeshProUGUI score;
    public TextMeshProUGUI operatorText;
    public TextMeshProUGUI problemNumber;
   
   //list to store numbers for the math problems
    public List<int> mathList = new List<int>();

    //random numbers for math problems
    public int randomFirstNumber;
    public int randomSecondNumber;

    //counter for the number of problems presented
    int numOfProblems = 1;

    //variables to store for current math problem
    int firstNumberInProblem;
    int secondNumberInProblem;
    float answerOne;
    int answerTwo;
    float answerThree;

    //score counter
    int scoreCount = 0; 

    
    //variables for the placement and correctness of answers
    int displayRandomAnswer;
    int randomAnswerPlacement;

    //correct answer position
    public int currentAnswer;

    int maxNumOfProbs =15;

    //UI text to show if you are correct or wrong
    public TextMeshProUGUI correctOrNot;

    //static counter to send your score to the database
    public static int correctPoints =0;

    //sound effect
    [SerializeField] private AudioSource correctSoundEffect;
    [SerializeField] private AudioSource wrongSoundEffect;

    //display the first math problem
    private void Start(){
        displayMathProblem();
       
    }

    public void displayMathProblem()
    {
        //generate first an second numbers in list
        randomFirstNumber = Random.Range(0, mathList.Count +1);
        randomSecondNumber = Random.Range(0, mathList.Count +1);

        //assign numbers 
        firstNumberInProblem = randomFirstNumber;
        secondNumberInProblem = randomSecondNumber;
        
        //get the operator selected
        int operation = mathSelection.selectOperator;

        //have the operatoration be selected as random
        if (operation == 4)
            operation = Random.Range(0, 4);
        
        //calculate the answer and display the operator
        if (operation == 0) //add
        {
            answerOne = firstNumberInProblem + secondNumberInProblem;
            operatorText.text = "+";
        }
        else if (operation == 1) //sub
        {
            answerOne = firstNumberInProblem - secondNumberInProblem;
            operatorText.text = "-";
        }
        else if (operation == 2) //multi
        {
            answerOne = firstNumberInProblem * secondNumberInProblem;
            operatorText.text = "*";
        }
        else if (operation == 3) //div
        {
            //making sure you never divide by 0 
            if (secondNumberInProblem == 0)
            {
                secondNumberInProblem = 1;
            }
            answerOne = (float)firstNumberInProblem / secondNumberInProblem;
            operatorText.text = "/";
        }


        //generate incorrect answers
        displayRandomAnswer = Random.Range(0,2);

        if(displayRandomAnswer == 0){
            answerTwo = (int)answerOne + Random.Range(1,5);
            answerThree = answerOne + 7;
        }
        else
        {
            answerTwo = (int)answerOne - Random.Range(1,5);
            answerThree = answerOne - 7;
        }
        
        //UI text for numbers in equation
        firstNumber.text = firstNumberInProblem.ToString();
        secondNumber.text = secondNumberInProblem.ToString();

       //randomly assigns the answers for the UI buttons
        randomAnswerPlacement = Random.Range(0,3);
       
        //sets the 3 answers in UI text
        if(randomAnswerPlacement == 0){
            answer1.text = operation == 3 ? answerOne.ToString("F1") : answerOne.ToString("F0"); 
            answer2.text = answerTwo.ToString();
            answer3.text = answerThree.ToString("F1");
            currentAnswer = 0;
        }
        else if(randomAnswerPlacement == 1){
            answer1.text = answerThree.ToString("F1");
            answer2.text = operation == 3 ? answerOne.ToString("F1") : answerOne.ToString("F0"); 
            answer3.text = answerTwo.ToString();
            currentAnswer = 1;
        } 
        else{
            answer1.text = answerTwo.ToString();
            answer2.text = answerThree.ToString("F1");
            answer3.text = operation == 3 ? answerOne.ToString("F1") : answerOne.ToString("F0");
            currentAnswer = 2;
        }

        //updating the UI variables
        score.text = scoreCount.ToString();
        problemNumber.text = numOfProblems.ToString();


    }

    //functions that check to see if the answers are correct
    //also increment score if correct
    //show UI text if correct or not 
    public void ButtonAns1(){
        if(currentAnswer ==0)
        {
            correctOrNot.enabled = true;
            correctOrNot.color = Color.green;
            correctOrNot.text = "Correct :)";
            Invoke("TurnoffText", 1);
            scoreCount++;
            correctSoundEffect.Play();

        }
        else
        {
            correctOrNot.enabled = true;
            correctOrNot.color = Color.red;
            correctOrNot.text = "Wrong :(";
            Invoke("TurnoffText", 1);
            wrongSoundEffect.Play();
        }  

        }


    public void ButtonAns2(){
        if(currentAnswer ==1)
        {
            correctOrNot.enabled = true;
            correctOrNot.color = Color.green;
            correctOrNot.text = "Correct :)";
            Invoke("TurnoffText", 1);
            scoreCount++;
            correctSoundEffect.Play();
        }  
        else
        {
            correctOrNot.enabled = true;
            correctOrNot.color = Color.red;
            correctOrNot.text = "Wrong :(";
            Invoke("TurnoffText", 1);
            wrongSoundEffect.Play();
        }  
    }
    public void ButtonAns3(){
        if(currentAnswer ==2 )
        {
            correctOrNot.enabled = true;
            correctOrNot.color = Color.green;
            correctOrNot.text = "Correct :)";
            Invoke("TurnoffText", 1);
            scoreCount++;
            correctSoundEffect.Play();
        }  
        else
        {
            correctOrNot.enabled = true;
            correctOrNot.color = Color.red;
            correctOrNot.text = "Wrong :(";
            Invoke("TurnoffText", 1);
            wrongSoundEffect.Play();

        }
    }   
    
    //function to turn off the correct wrong text and load the next problem
    public void TurnoffText()
        {
            if(correctOrNot != null)
                    correctOrNot.enabled = false;
            
            //incement the problem number and load next level
            numOfProblems++;
            if(numOfProblems <= maxNumOfProbs)
                displayMathProblem();
            else
            {
                correctPoints += scoreCount;
                nextLevel();
            }

        }
    
    public void nextLevel()
    {
        SceneManager.LoadScene("BOSS");
    }
    

}
