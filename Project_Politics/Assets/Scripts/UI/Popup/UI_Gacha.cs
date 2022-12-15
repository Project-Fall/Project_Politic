using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Gacha : UI_Popup
{
    enum Objects
    {
        EnvelopImage,
        Back,
        GachaButton,
    }

    void Start()
    {
        Bind<GameObject>(typeof(Objects));

        BindEvent(GetObject((int)Objects.Back), (PointerEventData) => Managers.UI.ClosePopup(this));
        BindEvent(GetObject((int)Objects.GachaButton), (PointerEventData) => Gacha());
    }

    private void Gacha()
    {
        GetObject((int)Objects.GachaButton).SetActive(false);
        GetObject((int)Objects.Back).GetComponent<Button>().interactable = false;
        GetObject((int)Objects.EnvelopImage).GetComponent<Animator>().SetBool("Open", true);
    }
}
