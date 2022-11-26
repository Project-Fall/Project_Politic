using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObject/GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] private Character _player;
    public Character Player { get { return _player; } }

    private DateTime _date = new DateTime(2024, 6, 1);
    [SerializeField] private int _passedTurn;
    public int PassedTurn { get { return _passedTurn; } }

    public string GetDateString() { return _date.AddMonths(_passedTurn).ToString("yyyy-MM"); }

    public DateTime GetDate()
    {
        return _date.AddMonths(_passedTurn);
    }

    public string SetDate(int addMonth)
    {
        _passedTurn += addMonth;
        return GetDateString();
    }
}
