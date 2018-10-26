using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartGame : MonoBehaviour
{

    public Toggle mapOfTheDayToggle;

    public void Update()
    {
        if (mapOfTheDayToggle.isOn)
        {
            GameManager.instance.mapOfTheDay = true;
        }
        else
        {
            GameManager.instance.mapOfTheDay = false;
        }
    }


    public void StartMain()
    {
        SceneManager.LoadScene(1);
    }
}
