using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWatcher : MonoBehaviour
{
    private int enemyTankDataTotal;
    private int playerTankDataTotal;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("CheckPlayers", 0f, 0.2f);
    }

    public void CheckPlayers()
    {
        
        enemyTankDataTotal = 0;
        playerTankDataTotal = 0;
        foreach (TankData item in GameManager.instance.enemyTankData)
        {
            if (item != null)
            {
                enemyTankDataTotal++;
            }
        }
        print("AIs: " + enemyTankDataTotal);
        foreach (TankData item in GameManager.instance.tankData)
        {
            if (item == null)
            {
                GameManager.instance.tankData.Remove(item);
            }
        }
        print("Players: " + GameManager.instance.tankData.Count);
        if (enemyTankDataTotal == 0 && GameManager.instance.tankData.Count == 1)
        {
            TankData winner = GameManager.instance.tankData[0].gameObject.GetComponent<TankData>();
            winner.GetComponent<Motor>().enabled = false;
            winner.winScreen.SetActive(true);
            winner.bestScoreText.gameObject.transform.parent = winner.winScreen.transform.parent;
            winner.scoreText.gameObject.transform.parent = winner.winScreen.transform.parent;
        }
    }


}
