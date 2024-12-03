// imports
using UnityEngine;
using UnityEngine.SceneManagement;

// move the player to the error scene for if the cloud they 
// chose was on the left and it was an incorrect response.
public class toLeftError : MonoBehaviour
{
    public void toLeftErrorScene()
    {
        SceneManager.LoadScene("lossLeft");
    }
}