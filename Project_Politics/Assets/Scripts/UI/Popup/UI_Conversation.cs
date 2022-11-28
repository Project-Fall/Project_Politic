using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Conversation : UI_Popup
{
    class Conversation
    {
        string name;
        List<string> script = new List<string>();
    }

    private Dictionary<string, string> scripts = new Dictionary<string, string>();
    string[] textScript = { "text1", "text2", "text3", "text4", "text5" };

    enum Texts
    {
        ConversationText,
        NameText,
    }

    void Start()
    {
        Bind<Text>(typeof(Texts));
        StartCoroutine(WaitForNextScript());
    }

    IEnumerator WaitForNextScript()
    {
        foreach(string script in textScript)
        {
            GetText((int)Texts.ConversationText).text = script;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
    }
}
