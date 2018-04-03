using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Grenade : MonoBehaviour
{

    public AudioClip explosionSound;

    public float fuseTimer;

    public float radius;
    public float explosionForce;

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
            
        }
        
    }

    // When the pin is pulled
    void OnJointBreak()
    {
        fuse.Start();
    }

    // grenade explosion
    void Explosion()
    {
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        Instantiate(explosionEffect, grenade.transform.position, grenade.transform.rotation);



		foreach (Collider nearbyObject in Physics.OverlapSphere(grenade.transform.position, radius))
        {
            if (nearbyObject.GetComponent<Rigidbody>() != null)
            {
                RaycastHit hit;
				if (Physics.Raycast(grenade.transform.position, nearbyObject.transform.position - grenade.transform.position, out hit))
                {
					nearbyObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, grenade.transform.position, radius);
                    Target target = hit.transform.GetComponent<Target>();
                    if (target != null)
                    {
						float distance = Vector3.Distance(target.transform.position, grenade.transform.position);
                        target.TakeDamage(radius-distance);
                    }
                }

            }
            Destroy(grenade);
        }
    }
}
