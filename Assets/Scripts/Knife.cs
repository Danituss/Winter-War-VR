using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {
    public float damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision col)
    {
        
        Target target = col.gameObject.GetComponent<Target>();
        if (target != null)
        {
            Debug.Log(col);
            target.TakeDamage(damage);
        }
    }
}
