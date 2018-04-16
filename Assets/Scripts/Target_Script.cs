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

    public IEnumerator BringDown()
    {
        shotDown = true;
        /*
        while (rotator.transform.rotation.z >= -90f)
        {
            rotator.transform.Rotate(Vector3.back * Time.deltaTime * speed);
            yield return null;
        }*/

        rotator.transform.rotation = new Quaternion(0, 0, -45, 0);
        yield return null;

        Debug.Log("I am now downed.");
    }

    public IEnumerator BringUp()
    {
        shotDown = false;
        /*
        while (rotator.transform.rotation.z <= 0f)
        {
            rotator.transform.Rotate(Vector3.forward * Time.deltaTime * speed);
            yield return null;
        }*/

        rotator.transform.rotation = new Quaternion(0, 0, 0, 0);
        yield return null;

        Debug.Log("I am now up.");
    }

}
