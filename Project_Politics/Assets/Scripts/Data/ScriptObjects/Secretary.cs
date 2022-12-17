using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObject/Secretary")]
public class Secretary : ScriptableObject
{
    // 이름
    [SerializeField] private string _name;
    public string Name { get { return _name; } }

    [SerializeField] private int _level;
    public int Level { get { return _level; } set { _level = value; } }

    [SerializeField] private bool _used;
    public bool Used { get { return _used; } set { _used = value; } }

    [SerializeField] private int _salary;
    public int Salary { get { return _salary; } }

    // 열정 카리스마 공감 리더십 말빨
    [SerializeField] private int[] _stat = new int[5];
    public int[] Stat { get { return _stat; } set { _stat = value; } }

    public RuntimeAnimatorController Motion;
}
