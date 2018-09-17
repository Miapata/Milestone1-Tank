using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // instance
    public TankData tankData; // Get the tank data

    void Awake()
    {
        // Destroy using the singleton pattern
        if (instance != null)
        {

            Destroy(gameObject); // Destroy gameObject
        }
        else
        {
            instance = this; // set instance to this
            DontDestroyOnLoad(this);  // Don't destroy on load
        }
    }


}
