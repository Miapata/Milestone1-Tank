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

    [Space(5)]
    public Text bestScoreText;

    public Text scoreText;

    public Text livesText;

    //Health text
    public Text healthText;

    [Space(5)]
    //Move speed
    public float moveSpeed;

    //Rotate speed
    public float rotateSpeed;

    //Health
    public float health;

    //Amount of lives
    public int lives;

    //Rate of Fire
    public float rateOfFire;


    [Space(5)]
    public int score;

    // our best score
    private int bestScore;

    //Ai controller
    public AIController aiController;

    private void Start()
    {
        //Check if we are player 1 layer
        if (transform.root.gameObject.layer == 14)
        {
            //Get the best score
            bestScore = PlayerPrefs.GetInt("Player1BestScore");

        }
        //Check if we are in player 2 layer
        else if (transform.root.gameObject.layer == 15)
        {
            //Get the bestScore
            bestScore = PlayerPrefs.GetInt("Player2BestScore");
        }
        //If our best score is greater than 0, display it
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
        if (health <= 0 && lives <= 0)
        {
            if (GetComponent<Motor>().AI == false)
            {
                //Activate the lose screen
                loseScreen.SetActive(true);
            }
            // Destroy gameObject
            Destroy(transform.root.gameObject);
            GameObject tankExplodeSound = Instantiate(GameManager.instance.tankDiedSoundFX, transform.position, Quaternion.identity);
            Destroy(tankExplodeSound, 2f);
        }
        else if (health <= 0 && lives > 0)
        {
            lives--;
            GameObject tankExplodeSound = Instantiate(GameManager.instance.tankDiedSoundFX, transform.position, Quaternion.identity);
        }
        //Update our health text to current health
        healthText.text = "Health: " + health.ToString();

    }

    //In this method we add score if the player hits another tank
    public void AddScore()
    {

        //The score we add is 10
        score += 10;
        //Disaply the score
        scoreText.text = "Score: " + score.ToString();
        //If our score is greater than our best score, store it
        if (score > bestScore)
        {
            //Set our best score equal to our new score
            bestScore = score;
            //Check if we are player 1
            if (gameObject.transform.root.gameObject.layer == 14)
            {
                //Set the best score
                PlayerPrefs.SetInt("Player1BestScore", bestScore);
            }
            //Check if we are player 2
            else if (gameObject.transform.root.gameObject.layer == 15)
            {
                //Set the best score
                PlayerPrefs.SetInt("Player2BestScore", bestScore);
            }
        }

        //If the best score is not 0
        if (bestScore != 0)
        {
            //Display it to the player
            bestScoreText.text = "Best:" + bestScore.ToString();
        }

    }

    //This is used to check if there is only one playe remaining
    public void CheckPlayers()
    {
        if (gameObject != null)
        {
            //If our enemy tanks are empty and there is only one player tank
            if (GameManager.instance.enemyTankData.Length == 0 && GameManager.instance.tankData.Count == 1)
            {
                //Set the motor is disabled
                gameObject.GetComponent<Motor>().enabled = false;
                //Show the win screen
                winScreen.SetActive(true);
                //Show the best score text
                bestScoreText.gameObject.transform.parent = winScreen.transform.parent;
                //Show the score text
                scoreText.gameObject.transform.parent = winScreen.transform.parent;
            }
        }
    }

}


