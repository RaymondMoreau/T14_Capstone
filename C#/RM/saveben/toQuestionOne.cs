// imports
using UnityEngine;
using UnityEngine.SceneManagement;

// got question answer correct, moves on to next scene (question one)
public class toQuestionOne : MonoBehaviour
{
    public void toQuestionOneScene()
    {
        SceneManager.LoadScene("questionOne");
    }
}
