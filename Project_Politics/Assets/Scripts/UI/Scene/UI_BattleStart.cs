using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BattleStart : UI_Scene
{
    // Battle Scene에서
    public Action<PointerEventData> ButtonAction;
    public UI_CandidateInfo[] info = new UI_CandidateInfo[2];

    enum Objects
    {
        ElectionType,
        Left,
        Right,
    }

    void Start()
    {
        Bind<GameObject>(typeof(Objects));
        if ((Managers.Data.GameData.PassedTurn / 24) % 2 == 0)
            GetObject((int)Objects.ElectionType).GetComponent<Text>().text = "총선";
        else
            GetObject((int)Objects.ElectionType).GetComponent<Text>().text = "지방선거";

        GetObject((int)Objects.Left).GetOrAddComponent<UI_CandidateInfo>().Candidate
            = Managers.Scene.CurrentScene.GetComponent<BattleScene>().battleController.Candidates[0];

        GetObject((int)Objects.Right).GetOrAddComponent<UI_CandidateInfo>().Candidate
            = Managers.Scene.CurrentScene.GetComponent<BattleScene>().battleController.Candidates[1];

        BindEvent(Util.FindChild(transform.GetChild(0).gameObject, "BattleStartButton"), ButtonAction);
    }
}