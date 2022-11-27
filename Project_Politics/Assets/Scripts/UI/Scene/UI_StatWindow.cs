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
        //ToBattleButton,
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
    [SerializeField] private int _upFigure = 1;
    private MainController _mainController;

    private void Awake()
    {
        _mainController = new MainController();
    }

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
        //Button toBattle = GetButton((int)Buttons.ToBattleButton);
        //BindEvent(toBattle.gameObject, (PointerEventData eventData) => {
        //    Managers.Scene.LoadScene(Define.Scene.Battle);
        //    Managers.Data.GameData.SetDate(1);
        //    });
        //toBattle.interactable = false;

        // 날짜
        GetObject((int)Infos.CurrentDate).GetComponent<Text>().text = Managers.Data.GameData.GetDateString();

        // 인지도
        GetObject((int)Infos.Awareness).GetComponent<Text>().text = $"인지도 : {Managers.Data.Player.Awareness}";
    }

    private void UpdateAllScore()
    {
        for (int i = 0; i < Enum.GetValues(typeof(Scores)).Length; i++)
            UpdateScore(i);
    }

    private void UpdateScore(int idx)
    {
        GetText(idx).text = Managers.Data.Player.Stat[idx].ToString();
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

        Managers.Data.Player.Stat[idx] += _upFigure;
        UpdateScore(idx);

        // 날짜 변경, UI 적용
        GetObject((int)Infos.CurrentDate).GetComponent<Text>().text = Managers.Data.GameData.SetDate(1);

        // 선거 이벤트 오픈 날짜인지 비교 -> 현재 이것 때문에 버그 있음
        //GetButton((int)Buttons.ToBattleButton).interactable = _mainController.IsErect();
        
        // 선거 전 달 입후보 여부 질문
        if (_mainController.IsErect())
            Managers.UI.ShowPopup<UI_ElectionQuestion>();

        // 인지도 적용
        GetObject((int)Infos.Awareness).GetComponent<Text>().text = $"인지도 : {Managers.Data.Player.Awareness}";

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
