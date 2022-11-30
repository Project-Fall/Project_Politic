using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Json 파일에 들어갈 Format만 모아둠

[Serializable]
public class Content
{
    public string title;
    public List<Script> contents = new List<Script>();
}

[Serializable]
public class Script
{
    public string name;
    public string script;
}

[Serializable]
public class ScriptLoader : ILoader<string, List<Script>>
{
    public List<Content> scripts = new List<Content>();

    public Dictionary<string, List<Script>> MakeDict()
    {
        Dictionary<string, List<Script>> dic = new Dictionary<string, List<Script>>();
        foreach (Content content in scripts)
        {
            dic.Add(content.title, content.contents);
        }
        return dic;
    }
}