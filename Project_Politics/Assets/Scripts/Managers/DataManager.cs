using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private GameData _gameData;
    public GameData GameData { get { Init(); return _gameData; } }
    public Character Player { get { Init(); return GameData.Player; } }

    public Dictionary<string, List<Script>> ScriptData = new Dictionary<string, List<Script>>();

    public void Init()
    {
        if (_gameData == null)
        {
            _gameData = Managers.Resource.Load<GameData>("ScriptObjects/GameData");
            ScriptData = LoadJson<ScriptLoader, string, List<Script>>("Script").MakeDict();
        }        
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}