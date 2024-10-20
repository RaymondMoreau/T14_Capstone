using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AdditionMovementTest
{
    private GameObject characterObject;
    private AdditionCharacterActions characterActions;

    [SetUp]
    public void SetUp()
    {
        characterObject = new GameObject();
        var rigidbody = characterObject.AddComponent<Rigidbody2D>();
        characterActions = characterObject.AddComponent<AdditionCharacterActions>();

        characterActions.character = rigidbody;
        characterActions.lives = 3;
        characterActions.score = 0;

        var logicObject = new GameObject();
        var logicScript = logicObject.AddComponent<AdditionLogicScript>();
        characterActions.logic = logicScript;
    }

    [Test]
    public void GetLives_ShouldReturnCorrectLives()
    {
        int currentLives = characterActions.getLives();

        Assert.AreEqual(3, currentLives, "Lives at start should be 3");
    }

    [Test]
    public void LifeLost() 
    {
        int livesAfterLoss = characterActions.lifeLost();

        Assert.AreEqual(2, livesAfterLoss, "should decrease by 1 only");
    }

    //[Test]
    //public void Jump()
    //{
    //    characterActions.gameNotOver = true;
    //    float initialPosition = characterObject.transform.position.x;

    //    characterActions.jump();

    //    float newPosition = characterObject.transform.position.x;
    //    Assert.AreEqual(1.0, newPosition);

    //    //Assert.AreEqual(new Vector3(1,0), characterObject.transform.position);
    //}

    [Test]
    public void JumpWhileGameOver()
    {
        characterObject.transform.position = new Vector3(0, 0);
        characterActions.gameNotOver = false;

        characterActions.jump();

        Assert.AreEqual(new Vector3(0,0), characterObject.transform.position);
    }

    [TearDown]
    public void TearDown() 
    {
        Object.DestroyImmediate(characterObject);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void AdditionMovementTestSimplePasses()
    {
        // Use the Assert class to test conditions
        
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator AdditionMovementTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}