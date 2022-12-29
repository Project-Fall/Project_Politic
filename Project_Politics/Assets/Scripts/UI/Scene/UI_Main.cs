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
        ToOptionButton,
        SecretaryButton,
        //TestButton,
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
        Position,
    }

    private MainController _mainController;
    private bool _deployMode = false;

    private GameObject _statInfo;
    private GameObject _deploy;
    private GameObject _active;

    //public GameObject main;
    //public GameObject deploy;
    //public GameObject active;

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

        BindEvent(GetButton((int)Buttons.ToDeployButton).gameObject, (PointerEventData) => OnDeployModeWithoutAnim());
        BindEvent(GetButton((int)Buttons.ToOptionButton).gameObject, (PointerEventData) => Managers.UI.ShowPopup<UI_Option>());
        BindEvent(GetButton((int)Buttons.SecretaryButton).gameObject, (PointerEventData) => Managers.UI.ShowPopup<UI_MySecretary>());

        _statInfo = Util.FindChild(gameObject, "UI_StatInfo");
        _deploy = Util.FindChild(gameObject, "UI_Deploy");
        _active = Util.FindChild(gameObject, "UI_IsActive");

        Init();

        //BindEvent(GetButton((int)Buttons.TestButton).gameObject, (PointerEventData) => ChangeUI(true));

        Refresh();
    }

    private void Init()
    {
        _deploy.SetActive(false);
        _statInfo.SetActive(false);
        _active.SetActive(true);
        BindEvent(Util.FindChild(_active, "Rest", true), (PointerEventData) => OnRest());
        BindEvent(Util.FindChild(_active, "Active", true), (PointerEventData) => OnActive());
        if (Managers.Data.GameData.GetMoney() < 30)
            Util.FindChild(_active, "Rest", true).GetComponent<Button>().interactable = false;
    }

    private void OnRest()
    {
        Managers.Data.GameData.SetStress(UnityEngine.Random.Range(2, 5) * -20);
        Managers.Data.GameData.SetMoney(-30);
        Managers.Data.GameData.SetDate(1);
        Refresh();
    }

    private void OnActive()
    {
        _active.SetActive(false);
        _statInfo.SetActive(true);
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
        StartCoroutine(ShowEvent(eventData));

        // 스트레스 적용 (인맥일 때)
        if (eventData.pointerClick.name.Equals(Enum.GetName(typeof(Buttons), Buttons.ConnectionUpButton)))
        {
            Managers.Data.GameData.SetStress(-10);
            Managers.Data.GameData.SetMoney(-10);
        }   
        else
        {
            Managers.Data.GameData.SetStress(20);
            Managers.Data.GameData.SetMoney(UnityEngine.Random.Range(Managers.Data.GameData.Prize, Managers.Data.GameData.Prize + 10));
        }

        Managers.Data.GameData.SetDate(1);
        Refresh();

        //잠깐동안 버튼 이용 불가
        StartCoroutine(DisableButtons());
    }

    private IEnumerator ShowEvent(PointerEventData eventData)
    {
        Event evt = null;
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
        UI_Event ui_evt = Managers.UI.ShowPopup<UI_Event>();
        ui_evt.Init(evt);
        for (int i = 0; i < evt.Stat.Length; i++)
            Managers.Data.Player.Stat[i] += evt.Stat[i];

        yield return new WaitUntil(() => ui_evt.isEnd);

        // 선거 전 달 입후보 여부 질문
        if (_mainController.IsCandidacyDay())
            Managers.UI.ShowPopup<UI_ElectionQuestion>();
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
        GetObject((int)Infos.Money).GetComponent<Text>().text = $"자금 : {Managers.Data.GameData.GetMoney()}";

        // 날짜 변경, UI 적용
        GetObject((int)Infos.CurrentDate).GetComponent<Text>().text = Managers.Data.GameData.GetDateString();

        // 스트레스 UI 적용
        GetObject((int)Infos.Stress).GetComponent<Text>().text = $"스트레스 : {Managers.Data.GameData.Stress}";

        GetObject((int)Infos.Position).GetComponent<Text>().text = $"직책 : {Managers.Data.Player.Position}";

        Init();
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

    public void OnDeployModeWithoutAnim()
    {
        if (_deployMode == false)
        {
            GetButton((int)Buttons.ToDeployButton).image.color = Color.green;
            _deploy.SetActive(true);
            _statInfo.SetActive(false);
            _deployMode = true;
        }
        else
        {
            GetButton((int)Buttons.ToDeployButton).image.color = Color.white;
            _deploy.SetActive(false);
            _statInfo.SetActive(true);
            _deployMode = false;
        }
    }
}
