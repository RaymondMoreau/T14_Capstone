using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IslandSceneManager: MonoBehaviour //scene manager that loads the next correct scene when "continue" button is pressed
{
    public void loadInitalScene()
    {
        SceneManager.LoadScene("Initial Scene");
    }

    public void loadIslandScene()
    {
        SceneManager.LoadScene("Island Scene");
    }

    public void loadTreasureScene()
    {
        SceneManager.LoadScene("TreasureScene");
    }

    public void loadQuestionScene()
    {
        SceneManager.LoadScene("QuestionScene");
    }
}
