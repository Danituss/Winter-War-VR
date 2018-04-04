using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Image dmgOverlay;
    float dmgTaken = 0f;
    public Target healthCheck;
    float maxHp;
    

	
	void Start () {
        dmgOverlay.canvasRenderer.SetAlpha(dmgTaken);
        maxHp = healthCheck.health;
		
	}
	
	// Image in front of player get more red the more damage the player takes.
	void Update () {
      
            dmgTaken = 1 - (healthCheck.health / maxHp);
        
        dmgOverlay.canvasRenderer.SetAlpha(dmgTaken-0.05f);



    }
}
