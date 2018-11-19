using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    public GameObject[] gameObjects;
    // Use this for initialization
    void Start()
    {
        if (GameManager.instance.multiplayer == true)
        {

            print("Multiplayer mode");
            MultiplayerMode();
        }
    }

    void MultiplayerMode()
    {
        GameManager.instance.player2.SetActive(true);
        GameManager.instance.player1Camera.Reset();
        GameManager.instance.player1Camera.rect = new Rect(0, 0.5f, 0, 0);
        foreach (GameObject item in gameObjects)
        {
            if (item.activeInHierarchy == false)
            {
                item.SetActive(true);
            }
        }
    }


}
