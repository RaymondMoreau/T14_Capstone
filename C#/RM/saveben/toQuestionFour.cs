// imports
using UnityEngine;
using UnityEngine.SceneManagement;

// got question answer correct, moves on to next scene (question four)
public class toQuestionFour : MonoBehaviour
{
    public void toQuestionFourScene()
    {
        SceneManager.LoadScene("questionFour");
    }
}
