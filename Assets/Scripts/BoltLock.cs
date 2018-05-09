using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTK
{
    public class BoltLock : MonoBehaviour
    {
        Rigidbody rb;
        Vector3 startPos;
        bool upRight;
        bool beenBack;
        GameObject gun;
        GunMechanics gunscript;
        public Rigidbody emptyBullet;

        // Use this for initialization
        void Start()
        {
            upRight = false;
            rb = gameObject.GetComponent<Rigidbody>();
            gun = transform.parent.gameObject;
            gunscript = gun.GetComponent<GunMechanics>();
            SideWays();

        }

        // Update is called once per frame
        void Update()
        {

        }
        //When the bolt reaches the back of the weapon, it can move down
        void OnTriggerEnter(Collider col)
        {
            if (col.name == "Bolt Open")
            {
                beenBack = true;

                if (gunscript.currentAmmo > -1)
                {
                    //First cocking should not eject
                    if (gunscript.currentAmmo != gunscript.maxAmmo)
                    {
                        gunscript.EjectShell();
                    }

                    gunscript.currentAmmo--;
                }
            }

            if (col.name == "Bolt Locked" && beenBack)
            {
                gunscript.cocked = true;
                beenBack = false;
            }

            // call when bolt is coming from the sideways position to upright position
            if (col.gameObject.name == "BoltLock Trigger Front" && upRight == false)
            {
                Upwards();
                upRight = true;
                // called when bolt is coming from the back of the weapon back to the front
            }
            else if (col.gameObject.name == "BoltLock Trigger Front" && upRight == true)
            {
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
}
