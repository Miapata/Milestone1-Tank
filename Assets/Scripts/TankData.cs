﻿using System.Collections;
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

    //Ai controller
    public AIController aiController;

    // This method applys damage if we are hit
    public void ApplyDamage(float damage)
    {
        // Subtract the health
        health -= damage;

        //If the aiController is not null
        if (aiController != null)
        {
            //Flee
            aiController.SendMessage("StartFleeing");
        }

        // Check if health is lower than or 0
        if (health <= 0)
        {
            // Destroy gameObject
            Destroy(gameObject);
        }

    }
}


