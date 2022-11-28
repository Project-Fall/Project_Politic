using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ElectionQuestion : UI_Popup
{
    enum Buttons
    {
        YesButton,
        NoButton,
    }

    public Action<bool> ButtonClickAction;

    void Start()
    {
        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.YesButton).gameObject, (PointerEventData) => OnClickYes());
        BindEvent(GetButton((int)Buttons.NoButton).gameObject, (PointerEventData) => OnClickNo());
    }

    private void OnClickYes()
    {
        Managers.UI.ClosePopup();
        Managers.UI.ShowPopup<UI_Campain>();
    }

    private void OnClickNo()
    {
        gameObject.SetActive(false);
    }
}
