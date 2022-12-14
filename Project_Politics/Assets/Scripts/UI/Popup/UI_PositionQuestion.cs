using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PositionQuestion : UI_Popup
{
    enum Buttons
    {
        Lawmaker,
        Autonomy,
    }

    void Start()
    {
        Bind<Button>(typeof(Buttons));
        BindEvent(GetButton((int)Buttons.Lawmaker).gameObject, (PointerEventData) => OnClickLawmaker());
        BindEvent(GetButton((int)Buttons.Autonomy).gameObject, (PointerEventData) => OnClickAutonomy());
    }

    private void OnClickLawmaker()
    {
        Managers.UI.ClosePopup();
        Managers.UI.ShowPopup<UI_Campain>();
    }

    private void OnClickAutonomy()
    {
        gameObject.SetActive(false);
    }
}
