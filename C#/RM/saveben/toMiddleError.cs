// imports
using UnityEngine;
using UnityEngine.SceneManagement;

// move the player to the error scene for if the cloud they 
// chose was in the middle and it was an incorrect response.
public class toMiddleError : MonoBehaviour
{
    public void toMiddleErrorScene()
    {
        SceneManager.LoadScene("lossMiddle");
    }
}