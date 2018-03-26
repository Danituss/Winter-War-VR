using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public float health = 50f;


    // when an object with health gets damaged
    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }
    // when an object with health reaches 0 hitpoints
    void Die()
    {
        Destroy(gameObject);
    }
}
