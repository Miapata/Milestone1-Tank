using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public Rigidbody rigidBody;
    public GameObject explosion;
    public float missileSpeed;
	// Update is called once per frame
    void Start()
    {

        //After 10 seconds, destroy the missile
        Destroy(gameObject,10);

        //Set the velocity to go straight
        rigidBody.velocity = transform.right * missileSpeed;
    }

    void OnCollisionEnter(Collision other)
    {
        //Ifthe collider is not null
        if (other.collider != null)
        {
            //Instantiate the explosion
            Instantiate(explosion, transform.position, Quaternion.identity);

            //Destroy the gameobject
            Destroy(gameObject);
        }
    }
}
