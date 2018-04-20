namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ClipTrigger : MonoBehaviour
    {
        GameObject gun, clip;
        GunMechanics gunscript;
        //bool firstTime;

        // Use this for initialization
        void Start()
        {
            gun = transform.parent.gameObject;
            gunscript = gun.GetComponent<GunMechanics>();
            //Debug.Log("Gun name: " + gun.name);
            //firstTime = true;
        }

        // Check if ammo has run out and if so, destroy empty clip
        void Update()
        {
            //if (gunscript.currentAmmo < 1)
            //{
            //    foreach (Transform child in gun.transform) if (child.CompareTag("bullet"))
            //        {
            //            Destroy(this);
            //        }
            //    firstTime = true;
            //}
        }

        //Reload ammo if clip enters inner part of gun
        void OnTriggerEnter(Collider col)
        {

            if (col.gameObject.tag == "bullet" /*&& firstTime*/)
            {
                Debug.Log("Ammo Reloaded");
                gunscript.currentAmmo = gunscript.maxAmmo;
                //firstTime = false;
                Destroy(col.gameObject);
            }
        }
    }

}
