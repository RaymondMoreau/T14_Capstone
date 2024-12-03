using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouLose : MonoBehaviour
{
    // Start is called before the first frame update
    // get logic 
    public LogicManager logic;
    void Start()
    {
        //find logic object
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    // if the character collides with the incorrect answer, then this method will be triggered.
    // the method will load up scene #2 which is a scene saying "you lost" as selecting the wrong answer in the game means the player lost.
    private void OnTriggerEnter2D(Collider2D collision){

        //logic.addScore();
        SceneManager.LoadSceneAsync(2);


    }
}
