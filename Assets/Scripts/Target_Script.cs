using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Script : MonoBehaviour {
    public GameObject iAmTarget;
    public GameObject rotator;
    public bool shotDown;
    public float speed = 25;
    // Use this for initialization
    void Start ()
    {
        shotDown = false;
    }
	
	
	void Update () {

        //getting targets back up
        if (Input.GetMouseButtonDown(0) && shotDown == true)
        {
            rotator.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 180, 0), speed * Time.deltaTime);

            // rotator.transform.Rotate(0, -90, 0);
            shotDown = false;
        }

    }


    void OnTriggerEnter(Collider other)
        //when targets get hit
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * speed);
       // rotator.transform.Rotate(0,90, 0);
        shotDown = true;
    }
}
