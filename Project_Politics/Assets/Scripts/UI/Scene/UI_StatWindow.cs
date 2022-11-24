using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StatWindow : UI_Scene
{
    enum Buttons
    {
        PassionUpButton,
        CharismaUpButton,
        SympathyUpButton,
        LeadershipUpButton,
        TongueUpButton,
        ToBattleButton,
    }

    enum Scores
    {
        PassionScore,
        CharismaScore,
        SympathyScore,
        LeadershipScore,
        TongueScore,
    }

    enum Infos
    {
        CurrentDate,
        Awareness,
    }

    // 임시
    [SerializeField] private int[] _stat = { 10, 10, 10, 10, 10 };
    [SerializeField] private int _upFigure = 1;
    private DateTime _currentDate = new DateTime(2024, 6, 1);
    [SerializeField] private GameData _data;


    private int _awareness { get {
            int result = 0;
            foreach (int i in _stat)
                result += i;
            return result;
        } }

    void Start()
    {
        // 변경될 오브젝트 찾기
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Scores));
        Bind<GameObject>(typeof(Infos));

        // 현재 Status를 UI로 띄우기
        UpdateAllScore();

        // 버튼이랑 status 올리는 Action 연결
        for (int i = 0; i <= (int)Buttons.TongueUpButton; i++)
        {
            Action<PointerEventData> action = (PointerEventData eventData) => OnClickButton(eventData);
            BindEvent(GetButton(i).gameObject, action);
        }

        // Battle 버튼을 누르면 Battle Scene으로
        Button toBattle = GetButton((int)Buttons.ToBattleButton);
        BindEvent(toBattle.gameObject, (PointerEventData eventData) => Managers.Scene.LoadScene(Define.Scene.Battle));
        toBattle.interactable = false;

        // 24년 6월을 가정
        GetObject((int)Infos.CurrentDate).GetComponent<Text>().text = _currentDate.ToString("yyyy-MM");

        // 인지도
        GetObject((int)Infos.Awareness).GetComponent<Text>().text = $"인지도 : {_awareness}";
    }

    private void UpdateAllScore()
    {
        for (int i = 0; i < Enum.GetValues(typeof(Scores)).Length; i++)
            UpdateScore(i);
    }

    private void UpdateScore(int idx)
    {
        GetText(idx).text = _stat[idx].ToString();
    }

    private void ChangeButtonState(bool b)
    {
        for (int i = 0; i <= (int)Buttons.TongueUpButton; i++)
            GetButton(i).interactable = b;
    }

    private void OnClickButton(PointerEventData eventData)
    {
        int idx = new int();

        // Enum 대체 왜 string으로 안 바뀌는 거임?! 이해할 수 X
        switch (eventData.pointerClick.name)
        {
            case "PassionUpButton":
                idx = 0;
                break;
            case "CharismaUpButton":
                idx = 1;
                break;
            case "SympathyUpButton":
                idx = 2;
                break;
            case "LeadershipUpButton":
                idx = 3;
                break;
            case "TongueUpButton":
                idx = 4;
                break;
        }

        _stat[idx] += _upFigure;
        UpdateScore(idx);

        // 날짜 변경, UI 적용
        _currentDate = _currentDate.AddMonths(1);
        GetObject((int)Infos.CurrentDate).GetComponent<Text>().text = _currentDate.ToString("yyyy-MM");

        if (_currentDate.Equals(new DateTime(2026, 6, 1)))
            GetButton((int)Buttons.ToBattleButton).interactable = true;
        else
            GetButton((int)Buttons.ToBattleButton).interactable = false;

        // 인지도 적용
        GetObject((int)Infos.Awareness).GetComponent<Text>().text = $"인지도 : {_awareness}";

        //잠깐동안 버튼 이용 불가
        StartCoroutine(DisableButtons());
    }

    private IEnumerator DisableButtons()
    {
        ChangeButtonState(false);
        yield return new WaitForSeconds(1f);
        ChangeButtonState(true);
    }
}
