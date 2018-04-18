using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Script : MonoBehaviour {
    public GameObject iAmTarget;
    public GameObject rotator;
  //  public bool shotDown;
    public bool isDown;
    public float speed = 25;
    Animator animator;
    public GameObject target;
    // Use this for initialization
    void Start ()
    {
        animator = target.GetComponent<Animator>();
       // shotDown = false;
        isDown = true;
      
    }

    public void MakeFall()
    {
        isDown = true;
        Debug.Log("I am now downed.");
    }

    public void TargetRise()
    {
        isDown = false;
        Debug.Log("I am now up.");

    }
    private void Update()
    {
          animator.SetBool("isDown", isDown);

    }
    /*
    public IEnumerator BringDown()
    {
        shotDown = true;
        /*
        while (rotator.transform.rotation.z >= -90f)
        {
            rotator.transform.Rotate(Vector3.back * Time.deltaTime * speed);
            yield return null;
        }*/

    /*rotator.transform.rotation = new Quaternion(0, 0, -45, 0);
    yield return null;
    
    Debug.Log("I am now downed.");
    }*/
    /*
    public IEnumerator BringUp()
    {
        shotDown = false;
        
        while (rotator.transform.rotation.z <= 0f)
        {
            rotator.transform.Rotate(Vector3.forward * Time.deltaTime * speed);
            yield return null;
        }*/

    /*rotator.transform.rotation = new Quaternion(0, 0, 0, 0);
    yield return null;

    Debug.Log("I am now up.");
} */

}
