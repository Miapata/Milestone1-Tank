using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Use this for initialization
    void Awake()
    {
        //Destroy using the singleton pattern
        if (instance != null)
        {

            Destroy(gameObject);
        }
        else
        {
            instance = this;
			DontDestroyOnLoad(this);
        }
    }


}
