using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StatInfo : UI_Sub
{
    private MainController _mainController;

    void Start()
    {
        _mainController = Managers.Scene.CurrentScene.GetComponent<MainScene>().Controller;

        Bind<Button>(typeof(Define.Stat), Util.FindChild(gameObject, "Buttons"));
        Bind<Text>(typeof(Define.Stat), Util.FindChild(gameObject, "Scores"));

        // 버튼이랑 status 올리는 Action 연결
        for (int i = 0; i < Enum.GetValues(typeof(Define.Stat)).Length; i++)
        {
            Action<PointerEventData> action = (PointerEventData eventData) => OnClickButton(eventData);
            BindEvent(GetButton(i).gameObject, action);
        }
    }

    public void UpdateAllScore()
    {
        for (int i = 0; i < Enum.GetValues(typeof(Define.Stat)).Length; i++)
            UpdateScore(i);
    }

    public void UpdateScore(int idx)
    {
        GetText(idx).text = Managers.Data.Player.Stat[idx].ToString();
    }

    private void ChangeButtonState(bool b)
    {
        for (int i = 0; i < Enum.GetValues(typeof(Define.Stat)).Length; i++)
            GetButton(i).interactable = b;
    }


    private void OnClickButton(PointerEventData eventData)
    {
        // 이벤트 발생, 결과 적용
        StartCoroutine(ShowEvent(eventData));

        // 스트레스 적용 (인맥이 아닐 때)
        if (!eventData.pointerClick.name.Equals(Enum.GetName(typeof(Define.Stat), Define.Stat.Connection)))
            Managers.Data.GameData.SetStress(20);

        Managers.Data.GameData.SetDate(1);
        Managers.Data.GameData.SetMoney(UnityEngine.Random.Range(Managers.Data.GameData.Prize, Managers.Data.GameData.Prize + 10));

        //Refresh();

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
}
