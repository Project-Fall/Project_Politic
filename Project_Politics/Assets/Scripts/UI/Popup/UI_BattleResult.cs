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
        Light,
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
        BindEvent(GetObject((int)Objects.ReturnButton), (PointerEventData) => Managers.Scene.LoadScene(Define.Scene.Main));

        StartCoroutine(ShowResult());
    }

    private IEnumerator ShowResult()
    {
        GetObject((int)Objects.WinnerName).SetActive(false);
        GetObject((int)Objects.ReturnButton).SetActive(false);

        GetObject((int)Objects.Light).GetComponent<Animator>().Play("Light");
        yield return new WaitForSeconds(3f);

        GetObject((int)Objects.Light).GetComponent<Animator>().enabled = false;
        GetObject((int)Objects.LeftImage).SetActive(false);
        GetObject((int)Objects.RightImage).SetActive(false);
        yield return new WaitForSeconds(2f);

        ShowWinner();
        yield return new WaitForSeconds(1f);

        GetObject((int)Objects.ReturnButton).SetActive(true);
    }

    public void ShowWinner()
    {
        if (_battleController.GetWinner() == _battleController.Candidates[0])
            GetObject((int)Objects.LeftImage).SetActive(true);
        else
            GetObject((int)Objects.RightImage).SetActive(true);

        GetObject((int)Objects.WinnerName).SetActive(true);
    }
}