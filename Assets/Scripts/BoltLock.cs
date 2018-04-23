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
        bool hasBeenBack;
        GameObject gun;
        GunMechanics gunscript;
        public Rigidbody emptyBullet;

        // Use this for initialization
        void Start()
        {
            hasBeenBack = false;
            upRight = false;
            rb = gameObject.GetComponent<Rigidbody>();
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
            //If bolt is pulled back it will spawn emptybullet to jump from the gun
            if (transform.position.z <= -0.3)
            {
                Rigidbody emptyBullet_clone;
                emptyBullet_clone = Instantiate(emptyBullet, transform.position, Quaternion.identity);
                emptyBullet_clone.AddForce(Vector3.forward * 2, ForceMode.Impulse);
                hasBeenBack = true;
            }
            //When reloading motion is completed the gun is cocked
            if (hasBeenBack == true && transform.position.z >= -0.214)
            {
                gunscript.cocked = true;
                hasBeenBack = false;
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
