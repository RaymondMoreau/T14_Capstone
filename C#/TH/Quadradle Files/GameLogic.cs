using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using System;
using System.Text.RegularExpressions;

public class GameLogic : MonoBehaviour
{
    public GameObject gameWonScreen, gameOverScreen, userInputBoxes, submitButton, title, equationText;
    public TMP_InputField inputField1, inputField2, inputField3, inputField4, inputField5, inputField6;
    private TMP_InputField[] inputFields;
    public TextMeshProUGUI equation;
    public List<string> inputs =new List<string>();
    public int count = 1;
    public string[] correctAnswers = new string[2];
    private string[] questions;
    private string[,] answers;
    public bool winner = false;


    // Start is called before the first frame update
    public void Start() // questions and answers are set at the start of the gmae
    {
        questions = new string[] {"x^2 + 5x + 6", "x^2 - 4x - 12", "2x^2 + 7x + 3", "x^2 - 9x + 20",
        "3x^2 - 5x - 2", "x^2 + 6x + 9", "4x^2 - 8x + 3", "x^2 - 7x + 12", "x^2 - x - 6", "2x^2 + 3x - 5" };

        answers = new string[,] { {"x+2", "x+3" }, { "x-6", "x+2" }, { "2x+1", "x+3" },
            { "x-4", "x-5" }, { "3x+1", "x-2" }, { "x+3", "x+3" }, { "2x-3", "2x-1" },
            { "x-3", "x-4" }, { "x-3", "x+2" }, { "2x-5", "x+1" } };

        inputFields = new TMP_InputField[] { inputField1, inputField2, inputField3, inputField4, inputField5, inputField6 };
        foreach(TMP_InputField temp in inputFields)
        {
            temp.onValueChanged.AddListener(delegate { ValidateInput(temp); });
        }
        randomizeQuestion();
    }

    // Update is called once per frame
    void Update() //continously checking if the user won
    {
        enableRow(count);
        if(winner == true)
        {
            gameWonScreen.SetActive(true);
            userInputBoxes.SetActive(false);
            submitButton.SetActive(false);
            title.SetActive(false);
            equationText.SetActive(false);
        }
        if(count == 4)
        {
            gameOverScreen.SetActive(true);
            userInputBoxes.SetActive(false);
            submitButton.SetActive(false);
            title.SetActive(false);
            equationText.SetActive(false);
        }
    }
    void ValidateInput(TMP_InputField inputField)
    {
        //inputField.text = System.Text.RegularExpressions.Regex.Replace(inputField.text, "[^0-9]", "");
        //inputField.characterLimit = 2;
    }

    public void randomizeQuestion() //function that randomizes the question
    {
        int questionNum = UnityEngine.Random.Range(0, 10);
        Debug.Log(questionNum);
        equation.text = questions[questionNum];
        for (int i = 0; i < correctAnswers.Length; i++)
        {
            Debug.Log(answers[questionNum, i]);
            correctAnswers[i] = answers[questionNum, i];
        }
        
    }

    public void checkAnswers() //function that checks the answer
    {
        if ((inputs[inputs.Count - 2] == correctAnswers[0]) && (inputs[inputs.Count - 1] == correctAnswers[1]))
        {
            Debug.Log("CONGRATS!");
            winner = true;
        }else if ((inputs[inputs.Count - 2] == correctAnswers[1]) && (inputs[inputs.Count - 1] == correctAnswers[0]))
        {
            Debug.Log("CONGRATS!");
            winner = true;
        }
        inputs.Clear();
        count++;
    }

    public void ReadStringInput(string userInputs) //function that reads users input
    {
        userInputs = Regex.Replace(userInputs, "[ ()]", "");
        Debug.Log(userInputs);
        inputs.Add(userInputs);
    }

    public void enableRow(int row) //function that enables row based on attempt number
    {
        if (row == 1)
        {
            inputField1.interactable = true;
            inputField2.interactable = true;
            inputField3.interactable = false;
            inputField4.interactable = false;
            inputField5.interactable = false;
            inputField6.interactable = false;
        }
        if (row == 2)
        {
            inputField1.interactable = false;
            inputField2.interactable = false;
            inputField3.interactable = true;
            inputField4.interactable = true;
            inputField5.interactable = false;
            inputField6.interactable = false;
        }
        if (row == 3)
        {
            inputField1.interactable = false;
            inputField2.interactable = false;
            inputField3.interactable = false;
            inputField4.interactable = false;
            inputField5.interactable = true;
            inputField6.interactable = true;
        }
        if (row == 4)
        {
            inputField1.interactable = false;
            inputField2.interactable = false;
            inputField3.interactable = false;
            inputField4.interactable = false;
            inputField5.interactable = false;
            inputField6.interactable = false;
        }
    }
}