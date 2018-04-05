using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour {
    
    public Texture dmgOverlay;
    float dmgTaken = 0f;
    public Target healthCheck;
    float maxHp;
    public RawImage damageEffect;
    Color currColor;

    /// <summary>
    /// 
    /// </summary>
    void Start () {
        
        maxHp = healthCheck.health;
        currColor = damageEffect.color;

    }
	
	// Image on canvas that is in front of the player darkens as the player takes more damage
	void Update () {
        currColor.a = dmgTaken - 0.05f;
        dmgTaken = 1 - (healthCheck.health / maxHp);
        damageEffect.color = currColor;
        
        }
   
    
}
