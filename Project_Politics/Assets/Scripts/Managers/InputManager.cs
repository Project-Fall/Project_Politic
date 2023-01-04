using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public bool Click { get { return OneClick(); } }

    private long _lastClickTime = 0;
    public bool OneClick()
    {
        if (Input.GetMouseButtonDown(0) && DateTime.Now.Ticks - _lastClickTime > 2000000)
        {
            _lastClickTime = DateTime.Now.Ticks;
            return true;
        }

        return false;
    }

    public bool AnyKey()
    {
        return Input.anyKeyDown;
    }
}
