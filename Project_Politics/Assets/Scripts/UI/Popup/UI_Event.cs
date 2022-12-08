using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Event : UI_Popup
{
    [SerializeField] private Event _event;

    enum Images
    {
        EventImage,
    }

    public void Init(Event evt)
    {
        _event = evt;
        Bind<Image>(typeof(Images));
        GetImage((int)Images.EventImage).sprite = evt.Image;
        GetImage((int)Images.EventImage).preserveAspect = true;
        Managers.UI.ShowPopup<UI_Conversation>().Init(evt);
        StartCoroutine(WaitUntilClick());
    }

    IEnumerator WaitUntilClick()
    {
        while (true)
        {
            yield return new WaitUntil(() => Managers.Input.Click);
            Managers.UI.ClosePopup(this);
        }
    }
}
