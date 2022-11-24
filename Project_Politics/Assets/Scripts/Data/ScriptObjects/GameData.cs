using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObject/GameData")]
public class GameData : ScriptableObject
{
    // 플레이어
    [SerializeField] private Character _player;
    public Character Player { get { return _player; } }

    // 날짜 관련
    private DateTime _date = new DateTime(2024, 6, 1); // 초기 날짜
    [SerializeField] private int _passedTurn; // 지나간 개월

    public string GetDate() { return _date.AddMonths(_passedTurn).ToString("yyyy-MM"); }

    public string SetDate(int addMonth)
    {
        _passedTurn += addMonth;
        return GetDate();
    }
}
