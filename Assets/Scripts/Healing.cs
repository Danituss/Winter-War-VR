using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour {
    public float healingLeft;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    ///  When the Morphine stick is held close to the player, the players health is restored.
    /// </summary>
    
    void OnTriggerStay(Collider col)
    {
        
        
        if(col.gameObject.tag == "player" && healingLeft > 0)
        {
            Target targetToHeal = col.gameObject.GetComponent<Target>();
            if (targetToHeal.health < targetToHeal.maxHP)
            {
                healingLeft -= 0.2f;
                targetToHeal.health += 0.2f;
            }
        }
    }
}
