using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    //this method is used for an onclick button method, when the button will be clicked, this function takes place.
    // this function loads scene 1 which is the first scene of the game
    public void PlayGame()
   {
       SceneManager.LoadSceneAsync(1);
   }

   
}
