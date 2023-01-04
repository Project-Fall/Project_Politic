using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Button : UI_Sub
{
    [SerializeField] AudioClip ClickSE;
    [SerializeField] AudioClip HoverSE;
    [SerializeField] bool UseSE = true;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (UseSE)
        {
            if (ClickSE == null)
                BindEvent(gameObject, (PointerEventData) => { Managers.Sound.Play("ButtonClick"); });
            else
                BindEvent(gameObject, (PointerEventData) => { Managers.Sound.Play(ClickSE); });

            if (HoverSE == null)
                BindEvent(gameObject, (PointerEventData) => { Managers.Sound.Play("ButtonHover"); }, Define.UIEvent.Hover);
            else
                BindEvent(gameObject, (PointerEventData) => { Managers.Sound.Play(HoverSE); }, Define.UIEvent.Hover);
        }
    }
}
