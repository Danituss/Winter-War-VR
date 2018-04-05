namespace VRTK.examples
{
    using UnityEngine;
    using System.Diagnostics;

    public class Grenade : VRTK_InteractableObject
    {

        public AudioClip explosionSound;

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

        // grenade explosion
        void Explosion()
        {

            AudioSource.PlayClipAtPoint(explosionSound, transform.position, 1f);
            GameObject clone = Instantiate(explosionEffect, grenade.transform.position, grenade.transform.rotation);
            Destroy(clone, 2);



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
                Destroy(grenade.transform.parent.gameObject);
            }
        }
    }
}
