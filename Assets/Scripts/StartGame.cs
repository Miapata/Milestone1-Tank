using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartGame : MonoBehaviour
{
    //Map of the day toggleButton
    public Toggle mapOfTheDayToggle;
    //Input field for our text
    public InputField seedInput;
    public void Update()
    {

        //If the toggle is on
        if (mapOfTheDayToggle.isOn)
        {
            //Set mapoftheday to true
            GameManager.instance.mapOfTheDay = true;
            //set the seedInput gameobject active state to false
            seedInput.gameObject.SetActive(false);
        }
        else
        {
            //Set mapoftheday to false
            GameManager.instance.mapOfTheDay = false;
            //set the seedInput gameobject active state to true
            seedInput.gameObject.SetActive(true);
        }
    }


    //StartMain loads the level
    public void StartMain()
    {
        //If the mapoftheday toggle is not on
        if (!mapOfTheDayToggle.isOn)
        {
            //seedText is equal to the inputField text
            GameManager.instance.seedText = seedInput.text;
        }

        //Load level scene
        SceneManager.LoadScene(1);
    }
}
