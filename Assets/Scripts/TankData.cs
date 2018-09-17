using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    //These are the variables for the tank
    public float moveSpeed;
    public float rotateSpeed;
    public float health;
    public float rateOfFire;

    // This method applys damage if we are hit
    public void ApplyDamage(float damage)
    {
        // Subtract the health
        health -= damage;

        // Check if health is lower than or 0
        if (health <= 0)
        {
            // Destroy gameObject
            Destroy(gameObject);
        }

    }
}


