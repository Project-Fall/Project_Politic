using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Conversation : UI_Popup
{ 
    private Event _event;
    private float _typingSpeed = 0.05f;
    Coroutine co_typing = null;

    enum Texts
    {
        ConversationText,
        NameText,
    }

    enum Objects
    {
        Name,
    }

    public void Init(Event evt)
    {
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(Objects));

        _event = evt;

        StartCoroutine(PrintScript());
    }

    private IEnumerator PrintScript()
    {
        // 이벤트 스크립트 본문 출력
        foreach(Script script in _event.GetEventScript())
        {
            // 이름 없으면 이름 창 꺼짐
            if (script.name == "")
                GetObject((int)Objects.Name).SetActive(false);
            else
            {
                GetObject((int)Objects.Name).SetActive(true);
                GetText((int)Texts.NameText).text = script.name;
            }

            co_typing = StartCoroutine(TypingEffect(script.script));

            yield return new WaitUntil(() => Managers.Input.Click && co_typing == null);
        }

        // 이벤트 후 스탯 변화 출력
        var result = _event.GetResultText();
        if (result.Equals("") == false)
        {
            GetObject((int)Objects.Name).SetActive(false);
            GetText((int)Texts.ConversationText).text = result;
            yield return new WaitUntil(() => Managers.Input.Click);
        }

        Managers.UI.ClosePopup(this);
    }

    private IEnumerator TypingEffect(string script)
    {
        Text text = GetText((int)Texts.ConversationText);
        text.text = "";

        foreach (char c in script.ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(_typingSpeed);
        }

        text.text = script;
        co_typing = null;
    }

}
