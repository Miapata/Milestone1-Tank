using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum ThePowerups
    {
        None,
        RapidFire,
        Health,
        Speed
    }

    public ThePowerups currentPowerup;


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tank")
        {
            other.GetComponent<Motor>().powerup = this;
            other.GetComponent<Motor>().Powerups();
            Destroy(gameObject);
        }
    }
}
