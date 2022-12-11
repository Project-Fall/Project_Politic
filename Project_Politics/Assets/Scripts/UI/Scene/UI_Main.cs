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
        Stress,
    }

    private MainController _mainController;
    private bool _deployMode = false;
    [SerializeField] private bool _deployUI;

    private void Awake()
    {
        _mainController = new MainController();
        _deployUI = GetComponentInChildren<UI_Deploy>();

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
        // 이벤트 발생, 결과 적용
        ShowEvent(eventData);

        // 스트레스 적용 (인맥이 아닐 때)
        if (!eventData.pointerClick.name.Equals(Enum.GetName(typeof(Buttons), Buttons.ConnectionUpButton)))
            Managers.Data.GameData.SetStress(20);

        Managers.Data.GameData.SetDate(1);

        Refresh();

        // 선거 전 달 입후보 여부 질문
        if (_mainController.IsCandidacyDay())
            Managers.UI.ShowPopup<UI_ElectionQuestion>();

        //잠깐동안 버튼 이용 불가
        StartCoroutine(DisableButtons());
    }

    private void ShowEvent(PointerEventData eventData)
    {
        Event evt = new Event();
        switch (eventData.pointerClick.name)
        {
            case "CharismaUpButton":
                evt = Managers.Data.Events[0];
                break;
            case "ProfessionalUpButton":
                evt = Managers.Data.Events[1];
                break;
            case "LeadershipUpButton":
                evt = Managers.Data.Events[2];
                break;
            case "ConnectionUpButton":
                evt = Managers.Data.Events[3];
                break;
            case "SympathyUpButton":
                evt = Managers.Data.Events[4];
                break;
        }
        Managers.UI.ShowPopup<UI_Event>().Init(evt);
        for (int i = 0; i < evt.Stat.Length; i++)
            Managers.Data.Player.Stat[i] += evt.Stat[i];
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
        GetObject((int)Infos.CurrentDate).GetComponent<Text>().text = Managers.Data.GameData.GetDateString();

        // 스트레스 UI 적용
        GetObject((int)Infos.Stress).GetComponent<Text>().text = $"스트레스 : {Managers.Data.GameData.Stress}";
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
