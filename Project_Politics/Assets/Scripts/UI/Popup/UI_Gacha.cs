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
        Resume,
    }

    void Start()
    {
        Bind<GameObject>(typeof(Objects));

        BindEvent(GetObject((int)Objects.Back), (PointerEventData) => Managers.UI.ClosePopup(this));
        BindEvent(GetObject((int)Objects.GachaButton), (PointerEventData) => StartCoroutine(Gacha()));
        GetObject((int)Objects.Resume).SetActive(false);
    }

    private IEnumerator Gacha()
    {
        GetObject((int)Objects.GachaButton).SetActive(false);
        GetObject((int)Objects.Back).GetComponent<Button>().interactable = false;
        GetObject((int)Objects.EnvelopImage).GetComponent<Animator>().SetBool("Open", true);

        yield return new WaitForSeconds(2f);
        GetObject((int)Objects.EnvelopImage).SetActive(false);
        GetObject((int)Objects.Resume).SetActive(true);
    }
}
