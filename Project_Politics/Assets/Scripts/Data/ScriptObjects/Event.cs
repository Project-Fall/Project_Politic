using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObject/Event")]
public class Event : ScriptableObject
{
    [SerializeField] private string _eventName;
    public string Name { get { return _eventName; } }


    // 열정 카리스마 공감 리더십 말빨 이벤트로 올라가는 수치
    [SerializeField] private int[] _stat = new int[5];
    public int[] Stat { get { return _stat; } }

    // 이미지
    public Sprite Image;


    // 대화 텍스트 선택
    public enum CONVERSATION_STATE{ EVENT1, EVENT2}
    [SerializeField] private CONVERSATION_STATE _state;
    public CONVERSATION_STATE State { get { return _state; } }
    
    //json 파일에서 대화 텍스트 가져오는 함수 만들기
    //

}