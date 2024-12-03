// imports
using UnityEngine;
using UnityEngine.SceneManagement;

// got question answer correct, moves on to next scene (question two)
public class toQuestionTwo : MonoBehaviour
{
    public void toQuestionTwoScene()
    {
        SceneManager.LoadScene("questionTwo");
    }
}
