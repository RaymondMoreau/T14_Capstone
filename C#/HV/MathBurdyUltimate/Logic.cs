


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//contains math logic

public class logic : MonoBehaviour
{
    //start is what happens when the scene starts
    void Start()
    {
        // call randomize question
        randomizeQuestion();
         
       
    }


     public Text questionText;
    public int number1, number2;
    [ContextMenu("Randomize Qustion")]
    //randomizes a question by randomly generating 2 numbers and then creating a multiplication question from it 
    public void randomizeQuestion()
    {
        number1 = Random.Range(2, 13);
        number2 = Random.Range(2, 13);
        questionText.text = number1.ToString() + " X " + number2.ToString();
        answerOptions();
     
    }

    public int option1, option2, correct;
    public Text textOption2, textOption1, correcttext;
    public int[] options; //could be removed from here (doesnt need to be public)
    [ContextMenu("Randomize Options")]
    //the answer options are created in this function.
    public void answerOptions()
    {
        //creating the 3 options - correct, option1, and option2
        correct = number1 * number2;
        option1 = (number1 - 1) * number2;
        option2 = (number1 + 1) * number2;

    //convert the int to string so that we can put it as text in unity game
        textOption1.text = option1.ToString();
        textOption2.text = option2.ToString();
        correcttext.text = correct.ToString();


    }
       
 
}
