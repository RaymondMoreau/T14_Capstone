using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

public class GameLogicTests
{
    private GameObject gameObject;
    private GameLogic gameLogic;


    [SetUp]
    public void SetUp() //tests set up
    {
        gameObject = new GameObject();
        gameLogic = gameObject.AddComponent<GameLogic>();

        gameLogic.equation = gameObject.AddComponent<TextMeshProUGUI>();

        GameObject inputField1Object = new GameObject(); 
        gameLogic.inputField1 = inputField1Object.AddComponent<TMP_InputField>();

        GameObject inputField2Object = new GameObject();
        gameLogic.inputField2 = inputField2Object.AddComponent<TMP_InputField>();

        GameObject inputField3Object = new GameObject();
        gameLogic.inputField3 = inputField3Object.AddComponent<TMP_InputField>();

        GameObject inputField4Object = new GameObject();
        gameLogic.inputField4 = inputField4Object.AddComponent<TMP_InputField>();

        GameObject inputField5Object = new GameObject();
        gameLogic.inputField5 = inputField5Object.AddComponent<TMP_InputField>();

        GameObject inputField6Object = new GameObject();
        gameLogic.inputField6 = inputField6Object.AddComponent<TMP_InputField>();
    }

    [Test]
    public void randomizeQuestionTest()
    {
        
        gameLogic.Start();

        Assert.IsNotEmpty(gameLogic.equation.text, "Random questions should be generated");
    }

    [Test]
    public void enableRowTest()
    {
        gameLogic.enableRow(1);

        Assert.IsTrue(gameLogic.inputField1.interactable && gameLogic.inputField2.interactable, "First 2 rows interactable");
        Assert.IsFalse(gameLogic.inputField3.interactable || gameLogic.inputField4.interactable || gameLogic.inputField5.interactable || gameLogic.inputField6.interactable, "Other input fields should be disabled.");
    }

    [Test]
    public void enableRow2Test()
    {
        gameLogic.enableRow(2);

        Assert.IsTrue(gameLogic.inputField3.interactable && gameLogic.inputField4.interactable, "Third and fourth input fields should be enabled.");
        Assert.IsFalse(gameLogic.inputField1.interactable || gameLogic.inputField2.interactable || gameLogic.inputField5.interactable || gameLogic.inputField6.interactable, "Other input fields should be disabled.");
    }

    [Test]
    public void enableRow3Test()
    {
        gameLogic.enableRow(3);

        Assert.IsTrue(gameLogic.inputField5.interactable && gameLogic.inputField6.interactable, "Fifth and sixth input fields should be enabled.");
        Assert.IsFalse(gameLogic.inputField1.interactable || gameLogic.inputField2.interactable || gameLogic.inputField3.interactable || gameLogic.inputField4.interactable, "Other input fields should be disabled.");
    }

    [Test]
    public void CheckCorrectAnswer()
    {
        // Arrange
        gameLogic.inputs.Add("x+2");
        gameLogic.inputs.Add("x+3");
        gameLogic.correctAnswers = new string[] { "x+2", "x+3" };
        int initialCount = gameLogic.count;

        // Act
        gameLogic.checkAnswers();

        // Assert
        Assert.IsTrue(gameLogic.winner, "Should Win game");
    }

    [Test]
    public void CheckIncorrectAnswer()
    {
        // Arrange
        gameLogic.inputs.Add("x+2");
        gameLogic.inputs.Add("x+4");
        gameLogic.correctAnswers = new string[] { "x+2", "x+3" };
        int initialCount = gameLogic.count;

        // Act
        gameLogic.checkAnswers();

        // Assert
        Assert.IsFalse(gameLogic.winner, "Should not win game");
    }

    [Test]
    public void ReadStringInput_ShouldRemoveSpacesAndParentheses()
    {
        // Arrange
        string input = "( x + 2 )";

        // Act
        gameLogic.ReadStringInput(input);

        // Assert
        Assert.AreEqual("x+2", gameLogic.inputs[0], "String input should remove spaces and parentheses.");
    }

    [TearDown]
    public void TeardDown()
    {
        Object.Destroy(gameObject);
    }

}
