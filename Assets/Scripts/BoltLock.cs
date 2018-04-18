using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltLock : MonoBehaviour
{
    Rigidbody rb;
    Vector3 startPos;
    bool atTheBack;

    // Use this for initialization
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        startPos = transform.localPosition;
        atTheBack = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        //Lock movement when bolt isn't upright
        if (rb.rotation.eulerAngles.z > 270)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;


        }
        //Unlock movement when bolt is upright
        else if (this.gameObject.transform.localPosition.z < startPos.z && atTheBack == false || rb.rotation.eulerAngles.z < 270 && atTheBack == false)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;


        }



    }
    //When the bolt reaches the back of the weapon, it can move down
    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.name == "BoltLock Trigger Back")
        {

            if (atTheBack == false)
            {
                atTheBack = true;
                rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
            }
            else
            {
                atTheBack = false;
            }

        }
        if (col.gameObject.name == "BoltLock Trigger Front")
        {

            if (atTheBack == false)
            {
                atTheBack = true;
                rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
            }
            else
            {
                atTheBack = false;
            }

        }

    }
}
