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
    public void ApplyDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
}


