namespace VRTK
{
    using UnityEngine;

    public class BoltActionSlide : VRTK_InteractableObject
    {
        private float restPosition;
        private float fireTimer = 0f;
        private float fireDistance = 0.05f;
        private float boltSpeed = 0.01f;

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
            if (other.name == "BoltLock Trigger Back") {
                GetComponentInParent<RealGunMechanics>().cocked = true;
                GetComponentInParent<RealGunMechanics>().EjectShell();
                Debug.Log("Cocked!");
            }
        }
    }
}
