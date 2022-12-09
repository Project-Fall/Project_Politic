using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Option : UI_Popup
{
    enum Objects
    {
        ClickBlocker,
        CloseButton,
    }
    
    void Start()
    {
        Bind<GameObject>(typeof(Objects));

        // X 버튼을 누르거나, 창 바깥을 누를 때 꺼짐
        BindEvent(GetObject((int)Objects.ClickBlocker), (PointerEventData) => Close());
        BindEvent(GetObject((int)Objects.CloseButton), (PointerEventData) => Close());
    }

    public void Close()
    {
        Managers.UI.ClosePopup(this);
    }
}
