using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWatcher : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("CheckPlayers", 0f, 0.1f);
    }

    public void CheckPlayers()
    {
        if (GameManager.instance.enemyTankData.Length == 0 && GameManager.instance.tankData.Count == 1)
        {
            TankData winner = GameManager.instance.tankData[0].gameObject.GetComponent<TankData>();
            winner.GetComponent<Motor>().enabled = false;
            winner.winScreen.SetActive(true);
            winner.bestScoreText.gameObject.transform.parent = winner.winScreen.transform.parent;
            winner.scoreText.gameObject.transform.parent = winner.winScreen.transform.parent;
        }
    }
}
