using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour {

	public float maxLifetime;
	public float lifetime;


	// Use this for initialization
	void Start () {
		lifetime = maxLifetime;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		lifetime = lifetime - Time.deltaTime;
		if (lifetime <= 0f) {
			lifetime = maxLifetime;
			gameObject.SetActive (false);
		}
	}
}
