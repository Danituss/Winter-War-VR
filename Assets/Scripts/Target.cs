using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public float health;
    public float maxHP;
    
    void Start()
    {
        maxHP = health;
    }
    void Update()
    {
        if (health >= maxHP)
        {
            health = maxHP;
        }
    }
    // when an object with health gets damaged
    public void TakeDamage(float amount)
    {
        health -= amount;
		gameObject.GetComponent<Renderer> ().material.color = Color.Lerp (Color.green, Color.red, health / maxHP);
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }
    // when an object with health reaches 0 hitpoints
    void Die()
    {
        if (gameObject.tag == "player")
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
