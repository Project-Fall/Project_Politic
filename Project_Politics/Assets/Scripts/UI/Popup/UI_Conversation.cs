using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Conversation : UI_Popup
{
    private List<Script> _scripts = new List<Script>();
    private Event _event;

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

    IEnumerator PrintScript()
    {
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

            GetText((int)Texts.ConversationText).text = script.script;
            yield return new WaitUntil(() => Managers.Input.Click);
        }

        string result = MakeResultText();
        if (result.Equals("") == false)
        {
            GetText((int)Texts.ConversationText).text = result;
            yield return new WaitUntil(() => Managers.Input.Click);
        }

        Managers.UI.ClosePopup(this);
    }

    private string MakeResultText()
    {
        string result = new string("");
        for(int i=0; i<_event.Stat.Length; i++)
        {
            if (_event.Stat[i] == 0)
                continue;

            result += $"{Enum.GetName(typeof(Define.StatKor), i)}이(가) {_event.Stat[i]}, ";
        }

        if (!result.Equals(""))
        {
            result = result.Remove(result.Length - 2);
            result += " 되었다.";
        }

        return result;
    }
}
