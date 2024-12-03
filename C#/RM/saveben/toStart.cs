// imports
using UnityEngine;
using UnityEngine.SceneManagement;

// brings player back to start from one of the loss screens
public class toStart : MonoBehaviour
{
    public void toStartScene()
    {
        SceneManager.LoadScene("startScreen");
    }
}