using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Grenade : MonoBehaviour {

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

	void Update ()
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
        Instantiate(explosionEffect, transform.position, transform.rotation);

       Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
            }
        }
        Destroy(grenade);
    }    
}
