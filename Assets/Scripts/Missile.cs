using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public Rigidbody rigidBody;
    public GameObject explosion;
    public float missileSpeed;
    public float damage;
    // Update is called once per frame
    void Start()
    {

        //After 10 seconds, destroy the missile
        Destroy(gameObject, 10);

        //Set the velocity to go straight
        rigidBody.velocity = transform.right * missileSpeed;
    }

    void OnCollisionEnter(Collision other)
    {
        // If the collider is not null
        if (other.collider != null && other.gameObject != gameObject)
        {

            // Instantiate the explosion
            Instantiate(explosion, transform.position, Quaternion.identity);

            // Destroy the gameobject
            Destroy(gameObject);

            if (other.gameObject.tag == "Tank") // Check if the tag is Tank
            {
                // Apply damage using the method
                other.gameObject.GetComponent<TankData>().ApplyDamage(damage);
            }

        }
    }
}
