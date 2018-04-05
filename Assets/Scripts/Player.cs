using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {
    
    public Texture dmgOverlay;
    float dmgTaken = 0f;
    public Target healthCheck;
    float maxHp;
    private Color guiColor;

	/// <summary>
    /// 
    /// </summary>
	void Start () {
        
        maxHp = healthCheck.health;
        guiColor = Color.white;

    }
	
	// GUI Texture darkens as the player takes more damage
	void Update () {
        guiColor.a = dmgTaken - 0.1f;
        dmgTaken = 1 - (healthCheck.health / maxHp);
        
        

        
    }
    public Vector3 WorldToGuiPoint(Vector3 position)
    {
        var guiPosition = Camera.main.WorldToScreenPoint(position);
        guiPosition.y = Screen.height - guiPosition.y;

        return guiPosition;
    }
    /// <summary>
    /// draws the damageTexture
    /// </summary>
    void OnGUI()
    {
        GUI.color = guiColor;
        GUI.DrawTexture(new Rect(0, 0,Screen.width, Screen.height), dmgOverlay);
        
    }
}
