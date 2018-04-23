using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltLock : MonoBehaviour
{
    Rigidbody rb;
    Vector3 startPos;
    bool upRight;
   

    // Use this for initialization
    void Start()
    {
        upRight = false;
        rb = gameObject.GetComponent<Rigidbody>();
        
        SideWays();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //When the bolt reaches the back of the weapon, it can move down
    void OnTriggerEnter(Collider col)
    {

        // call when bolt is coming from the sideways position to upright position
        if (col.gameObject.name == "BoltLock Trigger Front" && upRight == false)
        {
            Upwards();
            upRight = true;
        // called when bolt is coming from the back of the weapon back to the front
        } else if (col.gameObject.name == "BoltLock Trigger Front" && upRight == true) {
            SideWays();
            upRight = false;
        }
       
    }
    void SideWays()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
        rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
        
    }
    void Upwards()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
       
    }
}
