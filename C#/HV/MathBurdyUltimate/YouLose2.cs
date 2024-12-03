using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouLose2 : MonoBehaviour
{
    // Start is called before the first frame update
    public LogicManager logic;

    
    void Start()
    {
        // set logic object
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
    

        
    }

    //when burdy goes out of bounds either too high or too low, this method will be triffed.

    private void OnTriggerEnter2D(Collider2D collision){

        //logic.addScore();
        // load scene which says "oops" (for out of bounds)
        SceneManager.LoadSceneAsync(5);


    }
}
