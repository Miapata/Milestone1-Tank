using System.Collections;
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

    //Power up time
    public float powerupTime;

    //the powerup
    public Powerup powerup;

    //Player 2
    public bool player2;

    public InputManager inputManager;

    // used for the rate of fire
    private float nextEventTime;

    // Use this for initialization
    void Start()
    {
        // set the time event
        nextEventTime = Time.time;
        //set the text to the current health
        if (tankData.healthText != null)
        {
            tankData.healthText.text = "Health: " + tankData.health.ToString();
        }

        if (gameObject.layer != 11)
        {
            //If we are not player 2
            if (!player2)
            {
                //Set the keys to player 1: WASD
                inputManager.Player1();
            }
            else
            {
                //Set the keys to player2: Arrows
                inputManager.Player2();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        //If we are not the AI
        if (!AI && GameManager.instance.multiplayer == false)
        {
            // Here we get the vertical float 
            float vertical = Input.GetAxis("Vertical");

            // The character controller moves using the speed
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
        else if (GameManager.instance.multiplayer == true)
        {
            //Move
            Move();
            //Rotate
            Rotate();
            //Fire a missile
            Fire();
        }



    }

    //Rotate the player
    void Rotate()
    {
        //If left is pressed
        if (Input.GetKey(inputManager.left))
        {
            //Rotate
            transform.Rotate(0, -1 * tankData.rotateSpeed, 0);
        }
        //If right is pressed
        if (Input.GetKey(inputManager.right))
        {
            //rotate
            transform.Rotate(0, 1 * tankData.rotateSpeed, 0);
        }
    }

    //Move the player
    void Move()
    {
        //If down is pressed
        if (Input.GetKey(inputManager.down))
        {
            //move the player
            transform.Translate(Vector3.forward * tankData.moveSpeed * Time.deltaTime);
        }

        //If up is pressed
        if (Input.GetKey(inputManager.up))
        {
            //Move the player
            transform.Translate(Vector3.back * tankData.moveSpeed * Time.deltaTime);
        }
    }

    void Fire()
    {
        if (Input.GetKeyDown(inputManager.fire))
        {
            FireMissile();
        }
    }

    //FireMissile method
    public void FireMissile()
    {
        // check if we can fire
        if (Time.time >= nextEventTime)
        {
            // spawn the missile
            Rigidbody instance = Instantiate(missile, origin.transform.position, Quaternion.LookRotation(transform.right));
            instance.gameObject.layer = gameObject.layer;
            // set the the time we can fire again
            nextEventTime = Time.time + tankData.rateOfFire;
        }
    }

    //Powerups goes through all of powerups and calls a coroutine
    public void Powerups()
    {
        //Switch statement for the powerups
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
                //Start the speed coroutine
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
        if (tankData != null)
        {
            //Change the text of the health Text
            tankData.healthText.text = "Health: " + tankData.health.ToString();
        }

    }

    //IEnumerator for Rapid Fire
    public IEnumerator RapidFirePowerup()
    {
        //Set the rateOfFire lower
        tankData.rateOfFire = 0.3f;
        //wait for powerupTime
        yield return new WaitForSeconds(powerupTime);
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
        //Wait for powerupTime
        yield return new WaitForSeconds(powerupTime);
        //Set the moveSpeed to 5
        tankData.moveSpeed = 5;
        //Set the rotateSpeed to 3
        tankData.rotateSpeed = 3;
    }
}
