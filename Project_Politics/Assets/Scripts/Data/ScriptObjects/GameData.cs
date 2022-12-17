using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObject/GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] private Character _player;
    public Character Player { get { return _player; } }
    public List<Secretary> MyScretary = new List<Secretary>();

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

    [SerializeField] private int _money;
    public int GetMoney() { return _money; }
    public int AddMoney(int value) { return _money += value; }

    [SerializeField] private int _prizeMoney;
    public int Prize { get { return _prizeMoney; } set { _prizeMoney = value; } }

    [SerializeField] private int _stress;
    public int Stress { get { return _stress; } }
    public void SetStress(int value) { _stress += value; if (_stress > 100) _stress = 100; }

    public float BGMVolume = 1.0f;
    public float SEVolume = 1.0f;
}
