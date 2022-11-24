using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObject/GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] private Character _player;
    public Character Player { get { return _player; } }

    [SerializeField] private int _passedTurn;

    [SerializeField] private DateTime _date = new DateTime(2024, 6, 1);
    public DateTime Date { get { return _date; } }
}
