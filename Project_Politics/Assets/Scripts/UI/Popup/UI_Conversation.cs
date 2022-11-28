using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Conversation : UI_Popup
{
    class Conversation
    {
        string name;
    }

    private Dictionary<string, string> scripts = new Dictionary<string, string>();

    enum Texts
    {
        ConversationText,
        NameText,
    }

    void Start()
    {
        Bind<Text>(typeof(Texts));
    }
}
