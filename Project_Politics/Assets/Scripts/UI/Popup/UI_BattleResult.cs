using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BattleResult : UI_Popup
{
    private BattleController _battleController;

    enum Objects
    {
        Image1,
        Image2,
        WinnerName,
        ReturnButton,
        LeftImage,
        RightImage,
    }

    void Start()
    {
        _battleController = Managers.Scene.CurrentScene.GetComponent<BattleScene>().battleController;

        Bind<GameObject>(typeof(Objects));
        
        var resultCandidate = _battleController.GetWinner();
        GetObject((int)Objects.WinnerName).GetComponent<Text>().text = $"{resultCandidate.Name}\n당선";
        GetObject((int)Objects.Image1).GetComponent<Image>().sprite = _battleController.Candidates[0].WinImage;
        GetObject((int)Objects.Image2).GetComponent<Image>().sprite = _battleController.Candidates[1].WinImage;

        // 메인으로 돌아가는 버튼
        BindEvent(GetObject((int)Objects.ReturnButton), (PointerEventData eventData) => Managers.Scene.LoadScene(Define.Scene.Main));
    }

    public void WinnerLight()
    {
        if (_battleController.GetWinner() == _battleController.Candidates[0])
        {
            GetObject((int)Objects.LeftImage).SetActive(true);
            GetObject((int)Objects.RightImage).SetActive(false);
            Debug.Log("후보자 1 승리");
        }
        else
        {
            GetObject((int)Objects.LeftImage).SetActive(false);
            GetObject((int)Objects.RightImage).SetActive(true);
            Debug.Log("후보자 2 승리");
        }
    }
}
