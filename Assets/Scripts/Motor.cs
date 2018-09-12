using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Motor : MonoBehaviour {


    public Rigidbody missile;
    public GameObject origin;
    public CharacterController characterController;
    public float speed;
    public float rotateSpeed;
    private float lastEvent;
	// Use this for initialization
	void Start () {
            		
	}
	
	// Update is called once per frame
	void Update () {

        //Here we get the vertical float 
        float vertical= Input.GetAxis("Vertical");

        //The character cntroller moves using the speed
        characterController.SimpleMove(transform.forward*vertical*-speed);


        //We get the horizontal speed 
        float horizontal = Input.GetAxis("Horizontal");

        //Using the speed we rotate the player
        transform.Rotate(Vector3.up, horizontal*rotateSpeed);


        //If the space key is pressed
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody rocketInstance;
            //Instantiate the missile at the gameobject origin
            Instantiate(missile, origin.transform.position, Quaternion.LookRotation(transform.right));
            

        }
	}
}
