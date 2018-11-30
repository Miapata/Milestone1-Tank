using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;
    [Space(5)]
    public KeyCode fire;
    public void Player1()
    {
        left = KeyCode.A;
        right = KeyCode.D;
        up = KeyCode.W;
        down = KeyCode.S;
        fire = KeyCode.Space;
    }


    public void Player2()
    {
        left = KeyCode.LeftArrow;
        right = KeyCode.RightArrow;
        up = KeyCode.UpArrow;
        down = KeyCode.DownArrow;
        fire = KeyCode.RightControl;
    }


}
