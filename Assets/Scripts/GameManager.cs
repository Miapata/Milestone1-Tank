using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // instance
    public static GameManager instance;

    // Get the tank data
    public TankData tankData;

    // all of the enemy tank data
    public TankData[] enemyTankData;

    //All of the powerups
    public List<Powerup> powerups;

    // input from seed field
    public string seedText;

    //music Volume
    public float musicVolume;

    //Sound FX Volume
    public float soundFXVolume;

    //result
    public int seed;

    //check if we want to overide this GameManager
    public bool overrideThis;

    //Map of the day for map generation
    public bool mapOfTheDay;

    public bool multiplayer;

    void Awake()
    {
        // Destroy using the singleton pattern
        if (instance != null && overrideThis == true)
        {
            //Transfer data
            TransferData();
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

    //Transfer the data
    public void TransferData()
    {
        //Mapoftheday to the instance
        mapOfTheDay = GameManager.instance.mapOfTheDay;
        instance.mapOfTheDay = this.mapOfTheDay;
        //Tankdata to the instance
        instance.tankData = this.tankData;
        //enemyTankData to the instance
        instance.enemyTankData = this.enemyTankData;

    }

}
