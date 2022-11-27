using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Campain : UI_Popup
{
    enum Objects
    {
        LeafletCampain,
        CarCampain,
        TVCampain,
        ClickBlocker,
    }

    void Start()
    {
        Bind<GameObject>(typeof(Objects));

        GetObject((int)Objects.ClickBlocker).SetActive(false);
        for(int i=0; i<=(int)Objects.TVCampain; i++)
        {
            BindEvent(GetObject(i), (PointerEventData) => ElectionCampain());
        }
    }

    void ElectionCampain()
    {
        GetObject((int)Objects.ClickBlocker).SetActive(true);
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        Managers.Data.GameData.SetDate(2);
        Managers.Scene.LoadScene(Define.Scene.Battle);
    }
}
