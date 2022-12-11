using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    private GameData _gameData;
    public GameData GameData { get { Init(); return _gameData; } }
    public Character Player { get { Init(); return GameData.Player; } }

    public Dictionary<string, List<Script>> ScriptData = new Dictionary<string, List<Script>>();
    public List<Character> NPCs = new List<Character>();
    public List<Event> Events = new List<Event>();

    public void Init()
    {
        if (_gameData == null)
        {
            _gameData = Managers.Resource.Load<GameData>("ScriptObjects/GameData");
            ScriptData = LoadJson<ScriptLoader, string, List<Script>>("Script").MakeDict();
            BindAllNPC();
            BindAllEvent();
        }        
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    public void BindAllNPC()
    {
        string pattern = "*.asset";
        string[] names = Directory.GetFiles("Assets/Resources/ScriptObjects/Character/NPC", pattern);
        foreach (string n in names)
        {
            string name = Path.GetFileNameWithoutExtension(n);
            Character npc = Managers.Resource.Load<Character>($"ScriptObjects/Character/NPC/{name}");
            if (npc == null)
                continue;
            NPCs.Add(npc);
        }
    }

    public void BindAllEvent()
    {
        string pattern = "*.asset";
        string[] names = Directory.GetFiles("Assets/Resources/ScriptObjects/Event", pattern);
        foreach (string n in names)
        {
            string name = Path.GetFileNameWithoutExtension(n);
            Event evt = Managers.Resource.Load<Event>($"ScriptObjects/Event/{name}");
            if (evt == null)
                continue;
            Events.Add(evt);
        }
    }
}

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}