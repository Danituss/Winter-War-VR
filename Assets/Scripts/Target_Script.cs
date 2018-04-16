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

    }

   public IEnumerator BringDown(Transform target)
    {
        shotDown = true;
        while (rotator.transform.rotation.y != 90)
        {
            rotator.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * speed);
            // rotator.transform.Rotate(0,90, 0);
            yield return null;
        }

        print("Reached the target.");

        print("I am now downed.");
    }

    public IEnumerator BringUp(Transform target)
    {
        shotDown = false;
        while (rotator.transform.rotation.y != 0)
        {
            rotator.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * speed);
            yield return null;
        }

        print("Reached the target.");
       
        print("I am now up.");
    }

}
