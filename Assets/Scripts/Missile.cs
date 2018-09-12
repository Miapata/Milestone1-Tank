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
        Destroy(gameObject,10);
        rigidBody.velocity = transform.right * missileSpeed;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider != null)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
