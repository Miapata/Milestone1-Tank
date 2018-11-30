using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Volume : MonoBehaviour
{
    public static Volume instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Get all music
        GameObject[] music = GameObject.FindGameObjectsWithTag("Music");
        //Get all sound fx
        GameObject[] fx = GameObject.FindGameObjectsWithTag("FX");
        //Iterate through the music
        foreach (GameObject go in music)
        {
            //Adjust the volume
            go.GetComponent<AudioSource>().volume = GameManager.instance.musicVolume;
        }

        //Iterate through the fx
        foreach (GameObject go in fx)
        {
            //Adjust the volume
            go.GetComponent<AudioSource>().volume = GameManager.instance.soundFXVolume;
        }
    }
}
