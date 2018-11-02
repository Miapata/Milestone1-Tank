﻿using System.Collections;
using System.Collections.Generic;
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

        //If we are not the AI
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

    //Powerups goes through all of powerups and calls a coroutine
    public void Powerups()
    {
        //Seitch statement fo the powerups
        switch (powerup.currentPowerup)
        {
            //If powerups is none
            case Powerup.ThePowerups.None:
                break;
                //If powerup rapid fire
            case Powerup.ThePowerups.RapidFire:
                //Start the coroutine RapidFirePowerup
                StartCoroutine("RapidFirePowerup");
                break;
                //If powerup is Health
            case Powerup.ThePowerups.Health:
                //Start the coroutine Health powerup
                StartCoroutine("HealthPowerup");
                break;
                //If the powerup is Speed
            case Powerup.ThePowerups.Speed:
                //Start the speed coorutine
                StartCoroutine("SpeedPowerup");
                break;
            default:
                break;
        }



    }

    //IEnumerator for HealthPowerup
    public IEnumerator HealthPowerup()
    {
        //Wait
        yield return new WaitForSeconds(0);
        //Set health to 150
        tankData.health = 150;

    }

    //IEnumerator for Rapid Fire
    public IEnumerator RapidFirePowerup()
    {
        //Set the reateOfFire lower
        tankData.rateOfFire = 0.3f;
        //wait
        yield return new WaitForSeconds(5);
        //Set it back to normal
        tankData.rateOfFire = 1f;
    }

    //IEnumerator SpeedPowerUp
    public IEnumerator SpeedPowerup()
    {
        //Set the moveSpeed to 7
        tankData.moveSpeed = 7;
        //Set the rotateSpeed to 6
        tankData.rotateSpeed = 6;
        //Wait
        yield return new WaitForSeconds(5);
        //Set the moveSpeed to 5
        tankData.moveSpeed = 5;
        //Set the rotateSpeed to 3
        tankData.rotateSpeed = 3;
    }
}
