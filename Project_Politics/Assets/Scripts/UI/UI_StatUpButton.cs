using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UI_StatUpButton : UI_Base
{
    enum Buttons
    {
        Passion,
        Charisma,
        Sympathy,
        Leadership,
        Tongue,
    }

    void Start()
    {
        Bind<Button>(typeof(Buttons));

        for (int i = 0; i < _objects[typeof(Button)].Length; i++)
        {
            Action<PointerEventData> action = ((PointerEventData eventData) => { Debug.Log(eventData.pointerClick.name); });
            Button button = GetButton(i);
            
            AddUIEvent(button.gameObject, action);
        }
    }
}
