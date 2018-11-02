using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Motor : MonoBehaviour
{

    // Missile rigid body
    public Rigidbody missile;

    // The fire position
    public GameObject origin;

    // character controller
    public CharacterController characterController;

    // Check if the gameobject is an AI

    public bool AI;
    // tank data to get the variables
    public TankData tankData;

    public Powerup powerup;

    //Jesus
    // used for the rate of fire
    private float nextEventTime;

    // Use this for initialization
    void Start()
    {
        // set the time event
        nextEventTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (!AI)
        {
            // Here we get the vertical float 
            float vertical = Input.GetAxis("Vertical");

            // The character cntroller moves using the speed
            characterController.SimpleMove(transform.forward * vertical * -tankData.moveSpeed);


            // We get the horizontal speed 
            float horizontal = Input.GetAxis("Horizontal");

            // Using the speed we rotate the player
            transform.Rotate(Vector3.up, horizontal * tankData.rotateSpeed);


            // If the space key is pressed
            if (Input.GetKey(KeyCode.Space))
            {
                // Fire the missile
                FireMissile();

            }

        }



    }

    //FireMissile method
    public void FireMissile()
    {
        // check if we can fire
        if (Time.time >= nextEventTime)
        {
            // spawn the missile
            Instantiate(missile, origin.transform.position, Quaternion.LookRotation(transform.right));
            // set the the time we can fire again
            nextEventTime = Time.time + tankData.rateOfFire;
        }
    }

    public void Powerups()
    {
        switch (powerup.currentPowerup)
        {
            case Powerup.ThePowerups.None:
                break;
            case Powerup.ThePowerups.RapidFire:
                StartCoroutine("RapidFirePowerup");
                break;
            case Powerup.ThePowerups.Health:
                StartCoroutine("HealthPowerup");
                HealthPowerup();
                break;
            case Powerup.ThePowerups.Speed:
                StartCoroutine("SpeedPowerup");
                break;
            default:
                break;
        }



    }

    public IEnumerator HealthPowerup()
    {
        yield return new WaitForSeconds(0);
        tankData.health = 150;

    }

    public IEnumerator RapidFirePowerup()
    {
        tankData.rateOfFire = 0.3f;
        yield return new WaitForSeconds(5);
        tankData.rateOfFire = 1f;
    }

    public IEnumerator SpeedPowerup()
    {
        tankData.moveSpeed = 7;
        tankData.rotateSpeed = 6;
        yield return new WaitForSeconds(5);
        tankData.moveSpeed = 5;
        tankData.rotateSpeed = 3;
    }
}
