using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Image dmgOverlay;
    float dmgTaken = 0f;
    public Target healthCheck;
    float maxHp;
    

	// Use this for initialization
	void Start () {
        dmgOverlay.canvasRenderer.SetAlpha(dmgTaken);
        maxHp = healthCheck.health;
		
	}
	
	// Update is called once per frame
	void Update () {
        
        dmgTaken = 1 - (healthCheck.health / maxHp);
        Debug.Log(dmgTaken);
        dmgOverlay.canvasRenderer.SetAlpha(dmgTaken);



    }
}
