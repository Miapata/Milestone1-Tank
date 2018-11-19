using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TankData : MonoBehaviour
{

    //Health text
    public Text healthText;

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

    // This method applies damage if we are hit
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

        // Check if health is lower than or equal to 0
        if (health <= 0)
        {
            // Destroy gameObject
            Destroy(gameObject);
        }
        //Update our health text to current health
        healthText.text = "Health: " + health.ToString();

    }
}


