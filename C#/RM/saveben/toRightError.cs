// imports
using UnityEngine;
using UnityEngine.SceneManagement;

// move the player to the error scene for if the cloud they 
// chose was on the right and it was an incorrect response.
public class toRightError : MonoBehaviour
{
    public void toRightErrorScene()
    {
        SceneManager.LoadScene("lossRight");
    }
}