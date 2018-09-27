using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    //Move speed
    public float moveSpeed;

    //Rotate speed
    public float rotateSpeed;

    //Health
    public float health;

    //Rate of Fire
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


