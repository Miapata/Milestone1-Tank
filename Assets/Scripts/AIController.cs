using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject tank;
 
 

    // Update is called once per frame
    void Update()
    {

        SendMessage("FireMissile"); // sends message to Fire the missile

    }
}
