using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LogicScriptTest
{
    private GameObject logicGameObject;
    private LogicScript logicScript;

    [SetUp]
    public void SetUp()
    {
        //logicGameObject = new GameObject();
        //logicScript = logicGameObject.AddComponent<LogicScript>();

        //logicScript.allyShip = new GameObject();
        //logicScript.enemySpawner = logicGameObject.AddComponent<EnemyShipSpawner>();
        ////logicScript.scoreText = CreateTextMeshPro();
        ////logicScript.livesText = CreateTextMeshPro();
        ////logicScript.endScoreText = CreateTextMeshPro();
        ////logicScript.optionText1 = CreateTextMeshPro();
        ////logicScript.optionText2 = CreateTextMeshPro();
        ////logicScript.optionText3 = CreateTextMeshPro();
        ////logicScript.questionText = CreateTextMeshPro();
        //logicScript.gameOverPanel = new GameObject();
        //logicScript.gameOverPanel.SetActive(false);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(logicGameObject);
    }

    [Test]
    public void TestRandomizeQuestion()
    {
        //logicScript.randomizeQuestion();

        //Assert.IsTrue(logicScript.number1 >= 2 && logicScript.number1 <= 12);
        //Assert.IsTrue(logicScript.number2 >= 2 && logicScript.number2 <= 12);
        //Assert.AreEqual(logicScript.number1 + logicScript.number2, logicScript.correctLane());
    }
}
