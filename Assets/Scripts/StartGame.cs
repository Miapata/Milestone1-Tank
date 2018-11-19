using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    //StartMain loads the level
    public void StartMain()
    {


        //Load level scene
        SceneManager.LoadScene(1);
    }
}
