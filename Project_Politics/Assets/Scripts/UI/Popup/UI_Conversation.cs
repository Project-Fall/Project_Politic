using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Conversation : UI_Popup
{
    private List<Script> _scripts = new List<Script>();

    enum Texts
    {
        ConversationText,
        NameText,
    }

    enum Objects
    {
        Name,
    }

    void Start()
    {
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(Objects));

        _scripts = Managers.Data.ScriptData["title3"];
        StartCoroutine(WaitForNextScript());
    }

    IEnumerator WaitForNextScript()
    {
        foreach(Script script in _scripts)
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
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        gameObject.SetActive(false);
    }
}
