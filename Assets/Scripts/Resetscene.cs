using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resetscene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if(Input.GetKeyDown(KeyCode.Keypad1)) {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene(1);
        }
	}
}
