using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SphereCollider sphereCollider;
    public GameObject rapidFireGameObject;
    public float respawnTime = 20;
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

            if (currentPowerup == ThePowerups.RapidFire)
            {
                StartCoroutine("RespawnRapidFire", respawnTime);
            }
            else
            {
                StartCoroutine("Respawn", respawnTime);
            }
        }
    }

    public IEnumerator Respawn(float time)
    {
        spriteRenderer.enabled = false;
        sphereCollider.enabled = false;
        yield return new WaitForSeconds(time);
        spriteRenderer.enabled = true;
        sphereCollider.enabled = true;
    }
    public IEnumerator RespawnRapidFire(float time)
    {
        rapidFireGameObject.SetActive(false);
        sphereCollider.enabled = false;
        yield return new WaitForSeconds(time);
        rapidFireGameObject.SetActive(true);
        sphereCollider.enabled = true;
    }
}
