using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltLock : MonoBehaviour {
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = this.gameObject.GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update () {
        
        
       if (this.gameObject.transform.rotation.eulerAngles.z > 270)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationX;
        }
       else
        {
            rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        }
		
	}
}
