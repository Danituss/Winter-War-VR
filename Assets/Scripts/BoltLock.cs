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
        rb = gameObject.GetComponent<Rigidbody>();
        startPos = transform.localPosition;
        upRight = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(upRight);

        //Lock movement when bolt isn't upright
        if (rb.rotation.eulerAngles.z > 270 || upRight == false)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
         

        }
        //Unlock movement when bolt is upright
        else if (gameObject.transform.localPosition.z < startPos.z && upRight == true || rb.rotation.eulerAngles.z < 270 && upRight == true)
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
        
        
        if (col.gameObject.name == "BoltLock Trigger Front")
        {
                
                
            if (upRight == false) {
                upRight = true;
            }
            else if(upRight == true)
            {
                upRight = false;
            }
          

        }
   
    }
    }

