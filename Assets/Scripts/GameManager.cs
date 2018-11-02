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
    public bool overrideThis;
    public bool mapOfTheDay;
    void Awake()
    {
        // Destroy using the singleton pattern
        if (instance != null && overrideThis == true)
        {
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
        //Tankdata to the instance
        instance.tankData = this.tankData;
        //enemyTankData to the instance
        instance.enemyTankData = this.enemyTankData;
    }

}
