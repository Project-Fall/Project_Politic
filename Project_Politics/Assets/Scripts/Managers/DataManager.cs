using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    // 나중에 JSON으로 바꾸지 않을까 싶음
    private GameData _gameData;
    public GameData GameData { get { Init(); return _gameData; } }

    public Character Player { get { Init(); return GameData.Player; } }

    public void Init()
    {
        if (_gameData == null)
            _gameData = Managers.Resource.Load<GameData>("ScriptObjects/GameData");
    }

    //public T GetData<T>(string name) where T : ScriptableObject
    //{
    //    T data = Managers.Resource.Load<T>($"")
    //}
}
