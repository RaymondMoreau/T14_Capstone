using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SpinnerLogicTest
{
    private GameObject gameObject;
    private SpinnerLogicScript gameLogic;

    [SetUp]
    public void SetUp()
    {
        //gameObject = new GameObject();
        gameLogic = gameObject.AddComponent<SpinnerLogicScript>();
    }

    [Test]
    public void SpinnerStartsOnSpacePress()
    {
        gameLogic.spinWheel();

        Assert.IsTrue(gameLogic.isSpinning);
    }

    //[UnityTest]
    //public IEnumerator SpinnerStopTest()
    //{
    //    // Arrange
    //    var spinner = new GameObject().AddComponent<SpinnerLogicScript>();
    //    spinner.spinSpeed = 50f;
    //    spinner.decelerationSpeed = 1f;
    //    spinner.spinWheel();

    //    // Act & Assert
    //    while (spinner.isSpinning)
    //    {
    //        spinner.spinWheel();
    //        yield return null;
    //    }

    //    //Assert.AreEqual(0, spinner.currentSpeed);  // Spin speed should be 0 after stopping
    //    Assert.IsFalse(spinner.isSpinning);  // Spinner should no longer be spinning
    //}
}
