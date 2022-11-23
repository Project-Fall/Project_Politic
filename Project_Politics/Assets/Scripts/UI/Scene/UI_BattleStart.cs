using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BattleStart : UI_Scene
{
    // Battle Scene에서
    public Action<PointerEventData> ButtonAction;

    void Start()
    {
        BindEvent(Util.FindChild(transform.GetChild(0).gameObject, "BattleStartButton"), ButtonAction);
    }
}