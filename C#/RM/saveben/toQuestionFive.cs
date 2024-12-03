// imports
using UnityEngine;
using UnityEngine.SceneManagement;

// got question answer correct, moves on to next scene (question five)
public class toQuestionFive : MonoBehaviour
{
    public void toQuestionFiveScene()
    {
        SceneManager.LoadScene("questionFive");
    }
}
