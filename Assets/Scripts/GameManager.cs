using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // instance
    public static GameManager instance;

    // Get the tank data
    public TankData tankData; 

    void Awake()
    {
        // Destroy using the singleton pattern
        if (instance != null)
        {
            // Destroy gameObject
            Destroy(gameObject); 
        }
        else
        {
            // set instance to this
            instance = this;

            // Don't destroy on load
            DontDestroyOnLoad(this);  
        }
    }


}
