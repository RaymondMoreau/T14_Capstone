// imports
using UnityEngine;
using UnityEngine.SceneManagement;

// got question answer correct, moves on to next scene (question three)
public class toQuestionThree : MonoBehaviour
{
    public void toQuestionThreeScene()
    {
        SceneManager.LoadScene("questionThree");
    }
}
