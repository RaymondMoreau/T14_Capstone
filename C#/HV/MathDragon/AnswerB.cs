using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//class created for the action to be done when the character selects the correct answer.
public class AnswerB : MonoBehaviour
{
    // Start is called before the first frame update
    public LogicManager logic;
    void Start()
    {
        //grab logic object.
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    //this method will occur if the character collides with the correct answer.
    private void OnTriggerEnter2D(Collider2D collision){

        // increment the score (happens in logic.cs)
        logic.addScore();
        //load the next scene
        SceneManager.LoadSceneAsync(3);


    }
}
