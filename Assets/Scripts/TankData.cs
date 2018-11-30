using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TankData : MonoBehaviour
{
    //Our lose screen
    public GameObject loseScreen;
    //Our win screen
    public GameObject winScreen;

    public Text bestScoreText;

    public Text scoreText;

    //Health text
    public Text healthText;

    //Move speed
    public float moveSpeed;

    //Rotate speed
    public float rotateSpeed;

    //Health
    public float health;

    //Rate of Fire
    public float rateOfFire;

    public int score;

    private int bestScore;

    //Ai controller
    public AIController aiController;


    private void Start()
    {
        if (transform.root.gameObject.layer == 14)
        {
            bestScore = PlayerPrefs.GetInt("Player1BestScore");

        }
        else if (transform.root.gameObject.layer == 15)
        {
            bestScore = PlayerPrefs.GetInt("Player2BestSore");
        }
        if (bestScore > 0)
        {
            bestScoreText.text = "Best: " + bestScore.ToString();
        }
    }

    // This method applies damage if we are hit
    public void ApplyDamage(float damage)
    {
        // Subtract the health
        health -= damage;

        //If the aiController is not null
        if (aiController != null)
        {
            //Flee
            aiController.SendMessage("StartFleeing");
        }

        // Check if health is lower than or equal to 0
        if (health <= 0)
        {
            //Activate the lose screen
            loseScreen.SetActive(true);
            // Destroy gameObject
            Destroy(transform.root.gameObject);

        }
        //Update our health text to current health
        healthText.text = "Health: " + health.ToString();

    }

    public void AddScore()
    {

        score += 10;
        scoreText.text = "Score: " + score.ToString();
        if (score > bestScore)
        {
            bestScore = score;
            if (gameObject.transform.root.gameObject.layer == 14)
            {
                PlayerPrefs.SetInt("Player1BestScore", bestScore);
            }
            else if (gameObject.transform.root.gameObject.layer == 15 && score > bestScore)
            {
                PlayerPrefs.SetInt("Player2BestScore", bestScore);
            }
        }

        if (bestScore != 0)
        {
            bestScoreText.text = "Best:" + bestScore.ToString();
        }

    }
}


