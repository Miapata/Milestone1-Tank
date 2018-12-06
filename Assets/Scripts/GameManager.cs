using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // instance
    public static GameManager instance;

    public GameObject player2;
    [Space(5)]
    // Get the tank data
    public List<TankData> tankData;

    // all of the enemy tank data
    public TankData[] enemyTankData;

    //All of the powerups
    public List<Powerup> powerups;
    [Space(5)]
    // input from seed field
    public string seedText;

    [Space(5)]
    //music Volume
    public float musicVolume;

    //Sound FX Volume
    public float soundFXVolume;
    [Space(5)]
    //result
    public int seed;


    [Space(5)]
    //check if we want to override this GameManager
    public bool overrideThis;

    //Map of the day for map generation
    public bool mapOfTheDay;

    //multiplayer bool
    public bool multiplayer;


    [Space(5)]
    //Player 2 camera
    public Camera player2Camera;
    //Player 1 camera
    public Camera player1Camera;

    [Space(5)]
    public GameObject explosionSoundFX;
    public GameObject atmosphereMusic;
    public GameObject missileLaunchSoundFX;
    public GameObject clickSoundFX;
    public GameObject powerupSoundFX;
    public GameObject tankDiedSoundFX;
    public GameObject mouseDownSoundFX;
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



        instance.player1Camera = this.player1Camera;
        instance.player2Camera = this.player2Camera;
        instance.player2 = this.player2;


    }

}
