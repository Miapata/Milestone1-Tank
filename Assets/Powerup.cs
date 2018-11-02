using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {
    public enum ThePowerups
    {
        None,
        RapidFire,
        Health,
        Speed
    }

    public ThePowerups currentPowerup;

}
