using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObject/Secretary")]
public class Secretary : ScriptableObject
{
    // 이름
    [SerializeField] private string _name;
    public string Name { get { return _name; } }

    // 열정 카리스마 공감 리더십 말빨
    [SerializeField] private int[] _stat = new int[5];
    public int[] Stat { get { return _stat; } set { _stat = value; } }

    public RuntimeAnimatorController Motion;
}
