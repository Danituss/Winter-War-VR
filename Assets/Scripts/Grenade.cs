namespace VRTK.examples
{
    using UnityEngine;
    using System.Diagnostics;

    public class Grenade : VRTK_InteractableObject
    {
        public float fuseTimer;
        public float radius;
        public float explosionForce;
        public float damage;

        public GameObject grenade;
        public GameObject explosionEffect;

        Stopwatch fuse;

        void Start()
        {
            fuse = new Stopwatch();
        }

        void Update()
        {
            if (fuse.IsRunning && fuse.ElapsedMilliseconds >= fuseTimer * 1000f)
            {
                Explosion();
                Destroy(transform.parent.gameObject); //Destroy the pin after the explosion. Not the most elegant solution but it must be destroyed at some point.
            }
        }

        /// <summary>
        /// This method will be called when VRTK controller hovers on this' collider and presses trigger button.
        /// </summary>
        /// <param name="usingObject"></param>
        public override void StartUsing(VRTK_InteractUse usingObject)
        {
            fuse.Start();
            transform.parent.SetParent(null); //Put the pin to hierarchy root
            transform.parent.GetComponent<Rigidbody>().isKinematic = false; //Set the pin's rigidbody to not kinematic
        }

        /// <summary>
        /// Firstly will instantiate prefab which includes all the effects (particles, light, audio).
        /// After that will check for physics objects to interact with, adds force to them and gives damage if necessary.
        /// </summary>
        void Explosion()
        {
			GameObject clone = Instantiate(explosionEffect, grenade.transform.position, Quaternion.identity); //the clone handels it's selfdestruct.

            foreach (Collider nearbyObject in Physics.OverlapSphere(grenade.transform.position, radius))
            {
                if (nearbyObject.GetComponent<Rigidbody>() != null)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(grenade.transform.position, nearbyObject.transform.position - grenade.transform.position, out hit))
                    {
                        if (hit.collider == nearbyObject)
                            nearbyObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, grenade.transform.position, radius);
                        Target target = hit.transform.GetComponent<Target>();
                        if (target != null)
                        {
                            float distance = Vector3.Distance(target.transform.position, grenade.transform.position);
                            target.TakeDamage((radius - distance) * damage);
                        }
                    }
                }
                Destroy(grenade);
            }
        }
    }
}
