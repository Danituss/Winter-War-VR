using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is in testing for zoom feature.

public class Camera_Zoom_Script : MonoBehaviour
{
    public bool inScope = false;         // are we in scope?
    public Transform scopeLocation;      //where is the scope. referense for distance calculation
    private float basicView;             // basic field of view
    public float scopeZoomValue;         // amount the scope zooms
    public GameObject startScoping;      // Trigger to start scoping

    void Start()
    {
        basicView = Camera.main.fieldOfView;
        //Fetch the GameObject's Collider (make sure they have a Collider component)
       // startScoping = GameObject.FindGameObjectWithTag("currentScope");
      
        
        //Here the GameObject's Collider is not a trigger
       /* startScoping.isTrigger = false;
        //Output whether the Collider is a trigger type Collider or not
        Debug.Log("Trigger On : " + startScoping.isTrigger);*/
    }


    void Update()
    {
        // causes a zoom effect
        if (inScope == true)
        {
            checkDistance();
            Camera.main.fieldOfView = scopeZoomValue;
        }
        else
            //basic field of view
            Camera.main.fieldOfView = basicView;
    }


    void checkDistance()   //this method checs the distance between camera an the scope
    {

        scopeZoomValue = Vector3.Distance(scopeLocation.position, transform.position);
        scopeZoomValue = scopeZoomValue * 100;
        scopeZoomValue = scopeZoomValue-60;
        print("Distance to other: " + scopeZoomValue);

        if(scopeZoomValue > 60)
        {
            scopeZoomValue = 60;
        }
        if(scopeZoomValue < 20)
        {
            scopeZoomValue = 20;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        // when player camera enters scopes zoom start radius
        if (other.gameObject.name == "startScoping")
        {
            inScope = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //when player exits zoom radius
        if (other.gameObject.name == "startScoping")
        {
            inScope = false;
        }
    }
}




