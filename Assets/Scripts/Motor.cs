using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Motor : MonoBehaviour {

    
    public Rigidbody missile; // Missile rigid body
    public GameObject origin; // The fire position
    public CharacterController characterController; // character controller
    public bool AI; // Check if the gameobject is an AI
    public TankData tankData; // tank data to get the variables
    //Jesus
    private float nextEventTime; // used for the rate of fire
	// Use this for initialization
	void Start ()
	{
	    nextEventTime = Time.time; // set the time event
	}
	
	// Update is called once per frame
	void Update () {

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

	            FireMissile(); // Fire the missile

	        }
	    }



	}
    void FireMissile()
    {
        // check if we can fire
        if (Time.time >= nextEventTime)
        {
            //Jesus Christ 
            Instantiate(missile, origin.transform.position, Quaternion.LookRotation(transform.right)); // spawn the missile
            nextEventTime = Time.time + tankData.rateOfFire; // set the the time we can fire again
        }
    }
}
