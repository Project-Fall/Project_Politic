using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObject/Event")]
public class Event : ScriptableObject
{
    [SerializeField] private string _eventName;
    public string Name => _eventName;

    // 열정 카리스마 공감 리더십 말빨 이벤트로 올라가는 수치
    [SerializeField] private int[] _stat = new int[5];
    public int[] Stat => _stat;

    // 이미지
    public Sprite Image;

    // 대화 텍스트 선택
    public enum CONVERSATION_STATE{ EVENT1, EVENT2 }
    [SerializeField] private CONVERSATION_STATE _state;
    public CONVERSATION_STATE State => _state;

    //json 파일에서 대화 텍스트 가져오는 함수 만들기
    public List<Script> GetEventScript()
    {
        return Managers.Data.ScriptData[Name];
    }
    
    public string GetResultText()
    {
        var result = new StringBuilder("");
        for (var i = 0; i < Stat.Length; i++)
        {
            if (Stat[i] == 0)
                continue;
            if (Stat[i] > 0)
                result.Append($"{Enum.GetName(typeof(Define.StatKor), i)}이(가) {Stat[i]} 증가했습니다.\n");
            else
                result.Append($"{Enum.GetName(typeof(Define.StatKor), i)}이(가) {-Stat[i]} 감소했습니다.\n");
        }
        return result.ToString();
    }
}