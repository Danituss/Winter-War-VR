using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {
    public AudioClip explosionSound;
    public float timer;
    public float radius;
    public float explosionForce;

    bool timerStart = false;
    public GameObject grenade;
    public GameObject explosionEffect; 
    

    void Start()
    {
        
    }
	void Update () {
        if (timerStart == true)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            Explosion();
            timerStart = false;
            
        }
		
	}
    // When the pin is pulled
    void OnJointBreak()
    {
        timerStart = true;
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
