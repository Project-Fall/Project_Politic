using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler
{
    public Action<PointerEventData> OnPointClickHandler = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        // 버튼일 때 비활성화면 반응 X
        Button button = gameObject.GetComponent<Button>();
        if (button != null && button.interactable == false)
            return;

        if (OnPointClickHandler != null)
            OnPointClickHandler.Invoke(eventData);
    }
}