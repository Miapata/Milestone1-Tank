using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartGame : MonoBehaviour
{
    //Map of the day toggleButton
    public Toggle mapOfTheDayToggle;

    public void Update()
    {

        //If the toggle is on
        if (mapOfTheDayToggle.isOn)
        {
            //Set mapoftheday to true
            GameManager.instance.mapOfTheDay = true;
        }
        else
        {
            //Set mapoftheday to false
            GameManager.instance.mapOfTheDay = false;
        }
    }


    //StartMain loads the level
    public void StartMain()
    {
        SceneManager.LoadScene(1);
    }
}
