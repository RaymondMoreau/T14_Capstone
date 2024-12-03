using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Used for onclick button method. Will begin level 2 if the button is pressed. It loads the scene for Level2
public class Level2 : MonoBehaviour
{
    public void PlayLevel2()
   {
       SceneManager.LoadSceneAsync(0);
   }

   
}
