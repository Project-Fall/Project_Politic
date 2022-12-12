using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Battle : UI_Scene
{
    // 지금은 만들어놓고 가지고 오지만 나중에는 후보자 수만큼 생성하는 방식으로 바꿀 것 같음
    public GameObject CandidatePanel;
    [SerializeField] public int Speed = 1800;

    [SerializeField] Character[] Candidates;
    private int _moveCount = 0;
    private Vector2 _destPos;
    private bool _move = false;
    private GameObject _resultPanel;
    private BattleController _battleController;

    void Start()
    {
        _battleController = Managers.Scene.CurrentScene.GetComponent<BattleScene>().battleController;
        // 후보들을 가지고 옴 (씬으로부터)
        Candidates = _battleController.Candidates;
        Util.FindChild<UI_CandidateInfo>(CandidatePanel, "Candidate 1").Candidate = _battleController.Candidates[0];
        Util.FindChild<UI_CandidateInfo>(CandidatePanel, "Candidate 2").Candidate = _battleController.Candidates[1];

        // 위치 초기화
        CandidatePanel.transform.localPosition = Vector2.zero;
        _destPos = CandidatePanel.transform.localPosition;
        _destPos += new Vector2(-850, 0);

        // 결과창은 따로 관리
        _resultPanel = Util.FindChild(CandidatePanel, "BattleResult");
        _resultPanel.SetActive(false);
    }

    void Update()
    {
        if (_move)
        {
            CandidatePanel.transform.localPosition = Vector2.MoveTowards(CandidatePanel.transform.localPosition, _destPos, Speed * Time.deltaTime);
            if(CandidatePanel.transform.localPosition.Equals(_destPos))
            {
                _move = false;
                _destPos += new Vector2(-850, 0);
                _moveCount++;
            }
        }
        // 클릭 시 움직임 (후보자 수만큼 움직이면 더 안 움직임)
        if (Managers.Input.AnyKey() && _moveCount < Candidates.Length)
            _move = true;

        // 후보자 수만큼 움직이면 결과창 띄움
        if (_moveCount == Candidates.Length)
            ShowResult();
    }

    void ShowResult()
    {
        var resultCandidate = _battleController.GetWinner();
        Util.FindChild<Text>(_resultPanel, "Name").text = resultCandidate.Name;
        Util.FindChild<Image>(_resultPanel, "Image").sprite = resultCandidate.WinImage;

        // 메인으로 돌아가는 버튼
        BindEvent(Util.FindChild<Button>(_resultPanel, "ReturnButton").gameObject, 
            (PointerEventData eventData) => Managers.Scene.LoadScene(Define.Scene.Main));

        _resultPanel.SetActive(true);
    }
}
