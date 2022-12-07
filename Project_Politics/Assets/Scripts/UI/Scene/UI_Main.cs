using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Main : UI_Scene
{
    enum Buttons
    {
        CharismaUpButton,
        ProfessionalUpButton,
        LeadershipUpButton,
        ConnectionUpButton,
        SympathyUpButton,
        ToDeployButton,
    }

    enum Scores
    {
        CharismaScore,
        ProfessionalScore,
        LeadershipScore,
        ConnectionScore,
        SympathyScore,
    }

    enum Infos
    {
        CurrentDate,
        Awareness,
        Money,
    }

    [SerializeField] private int _upFigure = 1; // 이벤트로 이동 예정
    private MainController _mainController;
    private bool _deployMode = false;

    private void Awake()
    {
        _mainController = new MainController();

        // 변경될 오브젝트 찾기
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Scores));
        Bind<GameObject>(typeof(Infos));

        // 버튼이랑 status 올리는 Action 연결
        for (int i = 0; i <= (int)Buttons.SympathyUpButton; i++)
        {
            Action<PointerEventData> action = (PointerEventData eventData) => OnClickButton(eventData);
            BindEvent(GetButton(i).gameObject, action);
        }

        BindEvent(GetButton((int)Buttons.ToDeployButton).gameObject, (PointerEventData eventData) => OnDeployMode());

        Refresh();
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
        for (int i = 0; i <= (int)Buttons.SympathyUpButton; i++)
            GetButton(i).interactable = b;
    }

    private void OnClickButton(PointerEventData eventData)
    {
        int idx = new int();

        // Enum 대체 왜 string으로 안 바뀌는 거임?! 이해할 수 X
        switch (eventData.pointerClick.name)
        {
            case "CharismaUpButton":
                idx = 0;
                break;
            case "ProfessionalUpButton":
                idx = 1;
                break;
            case "LeadershipUpButton":
                idx = 2;
                break;
            case "ConnectionUpButton":
                idx = 3;
                break;
            case "SympathyUpButton":
                idx = 4;
                break;
        }

        Managers.Data.Player.Stat[idx] += _upFigure;
        Refresh();

        // 선거 전 달 입후보 여부 질문
        if (_mainController.IsCandidacyDay())
            Managers.UI.ShowPopup<UI_ElectionQuestion>();   

        Managers.UI.ShowPopup<UI_Event>().Init(Managers.Resource.LoadSO<Event>("Event/Event1"));

        //잠깐동안 버튼 이용 불가
        StartCoroutine(DisableButtons());
    }

    private IEnumerator DisableButtons()
    {
        ChangeButtonState(false);
        yield return new WaitForSeconds(1f);
        ChangeButtonState(true);
    }

    public void Refresh()
    {
        // Score UI 적용
        UpdateAllScore();

        // 인지도 UI 적용
        GetObject((int)Infos.Awareness).GetComponent<Text>().text = $"인지도 : {Managers.Data.Player.Awareness}";

        // 자금 변동, UI 적용
        Managers.Data.GameData.AddMoney(UnityEngine.Random.Range(1, 10));
        GetObject((int)Infos.Money).GetComponent<Text>().text = $"자금 : {Managers.Data.GameData.GetMoney()}";

        // 날짜 변경, UI 적용
        GetObject((int)Infos.CurrentDate).GetComponent<Text>().text = Managers.Data.GameData.SetDate(1);
    }

    public void OnDeployMode()
    {
        if (_deployMode == false)
        {
            GetButton((int)Buttons.ToDeployButton).image.color = Color.green;
            GetComponent<Animator>().Play("Deploy On");
            _deployMode = true;
        }
        else
        {
            GetButton((int)Buttons.ToDeployButton).image.color = Color.white;
            GetComponent<Animator>().Play("Deploy Off");
            _deployMode = false;
        }
    }
}
