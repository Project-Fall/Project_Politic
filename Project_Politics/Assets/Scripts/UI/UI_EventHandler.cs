using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public Action<PointerEventData> OnPointClickHandler = null;
    public Action<PointerEventData> OnPointEnterHandler = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        // 버튼일 때 비활성화면 반응 X
        Button button = gameObject.GetComponent<Button>();
        if (button != null && button.interactable == false)
            return;

        if (OnPointClickHandler != null)
            OnPointClickHandler.Invoke(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointEnterHandler != null)
            OnPointEnterHandler.Invoke(eventData);
    }
}