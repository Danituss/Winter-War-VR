﻿namespace VRTK
{
    using UnityEngine;

    public class BoltActionSlide : VRTK_InteractableObject
    {
        private float restPosition;
        private float fireTimer = 0f;
        private float fireDistance = 0.05f;
        private float boltSpeed = 0.01f;
        private bool beenBack;

        protected override void Awake()
        {
            base.Awake();
            restPosition = transform.localPosition.z;
        }

        protected override void Update()
        {
            base.Update();
            if (transform.localPosition.z >= restPosition)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, restPosition);
            }

            if (transform.localPosition.z < restPosition && !IsGrabbed())
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + boltSpeed);
            }

            if(IsGrabbed()) {
                GetComponent<Rigidbody>().isKinematic = false;
            } else {
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name == "BoltLock Trigger Back")
            {
                RealGunMechanics gun = GetComponentInParent<RealGunMechanics>();
                beenBack = true;

                if (gun.currentAmmo > -1)
                {
                    //First cocking should not eject
                    if (gun.currentAmmo != gun.maxAmmo)
                    {
                        gun.EjectShell();
                    }

                    gun.currentAmmo--;
                }
            }

            if (other.name == "BoltLock Trigger Front" && beenBack)
            {
                RealGunMechanics gun = GetComponentInParent<RealGunMechanics>();
                gun.cocked = true;
                beenBack = false;                
            }
        }
    }
}
