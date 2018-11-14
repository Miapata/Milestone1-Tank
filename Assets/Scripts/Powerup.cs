using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    //Sprite renderer
    public SpriteRenderer spriteRenderer;

    //Sphere collider
    public SphereCollider sphereCollider;

    //rapidFire Gameobject
    public GameObject rapidFireGameObject;

    //respawnTime
    public float respawnTime = 20;

    // the powerupTime 
    public float powerupTime;

    //ThePowerups 
    public enum ThePowerups
    {
        //None
        None,
        //RapidFire powerup
        RapidFire,
        //Health powerup
        Health,
        //Speed powerup
        Speed
    }

    //Current poweup
    public ThePowerups currentPowerup;


    private void Start()
    {
        //Add this to Powerup list
        GameManager.instance.powerups.Add(this);
    }

    //OnTriggerEnter for our sphere collider
    public void OnTriggerEnter(Collider other)
    {
        //If the tag is Tank
        if (other.gameObject.tag == "Tank")
        {
            //Set the motor's power up to this 
            other.GetComponent<Motor>().powerup = this;
            //Run the power up method
            other.GetComponent<Motor>().Powerups();

            //If the current powerup is rapid fire
            if (currentPowerup == ThePowerups.RapidFire)
            {
                //Start the rapid fire method
                StartCoroutine("RespawnRapidFire", respawnTime);
            }
            else
            {
                //Start the coroutine method
                StartCoroutine("Respawn", respawnTime);
            }
        }
    }

    //Respawn IEnumerator
    public IEnumerator Respawn(float time)
    {
        //spriteRenderer to false
        spriteRenderer.enabled = false;
        //Sphere collider to false
        sphereCollider.enabled = false;
        //Wait for respawn time
        yield return new WaitForSeconds(time);
        //Reset the two 
        spriteRenderer.enabled = true;
        sphereCollider.enabled = true;
    }
    //Used for rapid fire
    public IEnumerator RespawnRapidFire(float time)
    {
        //Gameobject to false
        rapidFireGameObject.SetActive(false);
        //Sphere collider to false
        sphereCollider.enabled = false;
        //Wait for time
        yield return new WaitForSeconds(time);
        //Reset the two to true
        rapidFireGameObject.SetActive(true);

        //Set the collider active
        sphereCollider.enabled = true;
    }
}
