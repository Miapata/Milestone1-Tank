using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Sounds : MonoBehaviour
{

    public static Sounds instance;
    public AudioSource audioPlayer;
    public AudioClip mouseDown;
    public AudioClip mouseUp;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioPlayer.PlayOneShot(mouseDown, GameManager.instance.soundFXVolume);
        }
        if (Input.GetMouseButtonUp(0))
        {
            audioPlayer.PlayOneShot(mouseUp, GameManager.instance.soundFXVolume);
        }
    }


}
