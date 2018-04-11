namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class Reload : MonoBehaviour
    {
        GameObject gun;
        Gun gunscript;


        // Use this for initialization
        void Start()
        {
            gun = transform.parent.gameObject;
            gunscript = gun.GetComponent<Gun>();
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(gun);

        }
        void OnTriggerEnter(Collider col)
        {
            
            if (col.gameObject.tag == "Ammo")
            {
                Debug.Log("toimiiko?");
                gunscript.ammo = 5;
                Destroy(col.gameObject);
            }
        }
    }
}