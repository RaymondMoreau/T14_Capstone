// imports
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// scene selector, after having so many scripts last game i was more efficient with my time
// and could use this script for all scene switches, where i could select the sceneName we
// are transitioning to via variable
public class toQ1 : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}


