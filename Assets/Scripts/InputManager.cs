using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Our input manager is used to set the different keys in multiplayer mode

    //Here are the keys we need
    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;
    [Space(5)]
    public KeyCode fire;
    public void Player1()
    {
        //Set the keys to WASD and space for fire
        left = KeyCode.A;
        right = KeyCode.D;
        up = KeyCode.W;
        down = KeyCode.S;
        fire = KeyCode.Space;
    }


    public void Player2()
    {
        //Set to the arrow keys and tight control for fire
        left = KeyCode.LeftArrow;
        right = KeyCode.RightArrow;
        up = KeyCode.UpArrow;
        down = KeyCode.DownArrow;
        fire = KeyCode.RightControl;
    }


}
