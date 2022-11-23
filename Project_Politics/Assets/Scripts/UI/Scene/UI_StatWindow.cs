using System;
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

    // 임시

    public int[] _status = { 10, 10, 10, 10, 10 };
    public int _upFigure = 1;

    void Start()
    {
        // 변경될 오브젝트 찾기
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Scores));

        // 현재 Status를 UI로 띄우기
        UpdateAllScore();

        // 버튼이랑 status 올리는 Action 연결
        for (int i = 0; i <= (int)Buttons.TongueUpButton; i++)
        {
            Action<PointerEventData> action = ((PointerEventData eventData) => OnClickButton(eventData));
            BindEvent(GetButton(i).gameObject, action);
        }

        // Battle 버튼을 누르면 Battle Scene으로
        BindEvent(GetButton((int)Buttons.ToBattleButton).gameObject,
            (PointerEventData eventData) => Managers.Scene.LoadScene(Define.Scene.Battle));
    }

    private void UpdateAllScore()
    {
        for (int i = 0; i < _objects[typeof(Text)].Length; i++)
            UpdateScore(i);
    }

    private void UpdateScore(int idx)
    {
        GetText(idx).text = _status[idx].ToString();
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

        _status[idx] += _upFigure;
        UpdateScore(idx);
    }
}
