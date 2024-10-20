using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class AdditionLogicTest
{
    private GameObject gameObject;
    private AdditionLogicScript additionLogic;
    private AdditionCharacterActions characterActions;

    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject();

        additionLogic = new AdditionLogicScript();

        additionLogic.livesText = new GameObject().AddComponent<TextMeshProUGUI>();
        additionLogic.time = new GameObject().AddComponent<TextMeshProUGUI>();
        additionLogic.questionText = new GameObject().AddComponent<TextMeshProUGUI>();
        additionLogic.textOption1 = new GameObject().AddComponent<TextMeshProUGUI>();
        additionLogic.textOption2 = new GameObject().AddComponent<TextMeshProUGUI>();
        additionLogic.textOption3 = new GameObject().AddComponent<TextMeshProUGUI>();

        characterActions = gameObject.AddComponent<AdditionCharacterActions>();
        additionLogic.character = characterActions;
    }

    [Test]
    public void RandomizeQuestion()
    {
        additionLogic.randomizeQuestionAddition();

        string questionText = additionLogic.questionText.text;
        Assert.IsNotNull(questionText);
        StringAssert.Contains("+", questionText, "Question should contain an addition operator.");

        string[] numbers = questionText.Split(new char[] { '+', ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
        int number1 = int.Parse(numbers[0]);
        int number2 = int.Parse(numbers[1]);

        Assert.GreaterOrEqual(number1, 2);
        Assert.LessOrEqual(number1, 12);
        Assert.GreaterOrEqual(number2, 2);
        Assert.LessOrEqual(number2, 12);
    }

    //[Test]
    //public void AnswerOptionsAddition()
    //{
    //    additionLogic.randomizeQuestionAddition();

    //    string[] options = new string[]
    //       {
    //        additionLogic.textOption1.text,
    //        additionLogic.textOption2.text,
    //        additionLogic.textOption3.text,
    //    };

    //    Assert.Contains((additionLogic.character.getLives() - 1 + additionLogic.character.getLives()).ToString(), options, "At least one option should be the correct sum.");
    //}

    [Test]
    public void updateTimer()
    {
        additionLogic.timeRemaining = 5.0f;
        additionLogic.timerRunning = true;

        additionLogic.Update();
        Assert.Less(additionLogic.timeRemaining, 5.0f, "Timer should count down.");
    }

    [Test]
    public void updateTimerEndsAtZero()
    {
        additionLogic.timeRemaining = 0.01f;

        additionLogic.Update();

        Assert.Less(additionLogic.timeRemaining, 0f, "Time remaining is zero when timer goes out");
        Assert.IsFalse(additionLogic.timerRunning, "Timer should stop running when time runs out.");
    }

    //[Test]
    //public void CheckAnswer1_ShouldJumpOnCorrectAnswer()
    //{
    //    // Arrange
    //    additionLogic.randomizeQuestionAddition();
    //    additionLogic.textOption1.text = additionLogic.number1 + additionLogic.number2.ToString();  // Ensure option1 is the correct answer

    //    // Act
    //    additionLogic.checkAnswer1();

    //    // Assert
    //    Assert.AreEqual(1, additionLogic.score, "Score should increment by 1 for correct answer.");
    //}

    [Test]
    public void GameOver_ActivatesGameOverScreen()
    {
        // Arrange
        additionLogic.gameOverScreen = new GameObject();
        additionLogic.questionOptions = new GameObject();

        // Act
        additionLogic.gameOver();

        // Assert
        Assert.IsTrue(additionLogic.gameOverScreen.activeSelf, "Game Over screen should be active.");
        Assert.IsFalse(additionLogic.questionOptions.activeSelf, "Question options should be hidden on Game Over.");
    }

    [Test]
    public void GameWon_ActivatesGameWonScreen()
    {
        // Arrange
        additionLogic.gameWonScreen = new GameObject();
        additionLogic.questionOptions = new GameObject();

        // Act
        additionLogic.gameWon();

        // Assert
        Assert.IsTrue(additionLogic.gameWonScreen.activeSelf, "Game Won screen should be active.");
        Assert.IsFalse(additionLogic.questionOptions.activeSelf, "Question options should be hidden on Game Won.");
    }


    [TearDown]
    public void TearDown() { 
        Object.DestroyImmediate(gameObject);
    }
}
