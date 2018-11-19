using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Options : MonoBehaviour
{
    //Music slider
    public Slider musicSlider;
    //Sound effect slider
    public Slider soundFxSlider;
    //Map of the day toggle
    public Toggle mapOfTheDayToggle;
    //Toggle for multiplayer
    public Toggle multiplayerToggle;
    //Input field for our seed
    public InputField seedText;
    //Our options panel
    public GameObject optionsPanel;   

    //Updates the soundFx volume
    public void UpdateSoundFX()
    {

        GameManager.instance.soundFXVolume = soundFxSlider.value;
    }

    //Updates the music volume
    public void UpdateMusicVolume()
    {
        GameManager.instance.musicVolume = musicSlider.value;
    }

    //Updates the multiplayer option
    public void UpdateMultiplayer()
    {
        GameManager.instance.multiplayer = multiplayerToggle.isOn;
    }

    //Updates our seed
    public void UpdateSeed()
    {
        GameManager.instance.seedText = seedText.text;
    }

    //Updates map of the day option
    public void UpdateMapOfTheDay()
    {
        GameManager.instance.mapOfTheDay = mapOfTheDayToggle.isOn;
    }

    //Disables our panel
    public void Disable()
    {
        optionsPanel.SetActive(false);
    }

    //Enables our panel
    public void Enable()
    {
        optionsPanel.SetActive(true);
    }
}
