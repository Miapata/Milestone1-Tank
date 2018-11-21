using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    public GameObject[] gameObjects;
    // Use this for initialization
    void Start()
    {
        //If we are in multiplayer mode, then Start the Multiplayer initailization
        if (GameManager.instance.multiplayer == true)
        {

            print("Multiplayer mode");
            MultiplayerMode();
        }
    }
    //Jesus Christ

    void MultiplayerMode()
    {
        //Set the player 2 status to active
        GameManager.instance.player2.SetActive(true);
        //Reset player 1 camera's settings
        GameManager.instance.player1Camera.Reset();
        //Set the cameras settings to a new rect
        GameManager.instance.player1Camera.rect = new Rect(0, 0.5f, 1, 1);
    }


}
