using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Battle : UI_Scene
{
    // 지금은 만들어놓고 가지고 오지만 나중에는 후보자 수만큼 생성하는 방식으로 바꿀 것 같음
    public GameObject CandidatePanel;
    public int Speed = 600;

    [SerializeField] Character[] Candidates;
    private int _moveCount = 0;
    private Vector2 _destPos;
    private bool _move = false;
    private GameObject _resultPanel;

    void Start()
    {
        // 후보들을 가지고 옴 (씬으로부터)
        Candidates = Managers.Scene.CurrentScene.GetComponent<BattleScene>().Candidates;

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
        if (Input.GetMouseButtonDown(0) && _moveCount < Candidates.Length)
            _move = true;

        // 후보자 수만큼 움직이면 결과창 띄움
        if (_moveCount == Candidates.Length)
            ShowResult();
    }

    void ShowResult()
    {
        // 인지도에 따라 당선자 이름 작성 (나중에 결과 판단 기준 바뀔 것 같음)
        if (Candidates[0].Awareness > Candidates[1].Awareness)
            Util.FindChild<Text>(_resultPanel, "Name").text = Candidates[0].Name;
        else
            Util.FindChild<Text>(_resultPanel, "Name").text = Candidates[1].Name;

        // 메인으로 돌아가는 버튼
        BindEvent(Util.FindChild<Button>(_resultPanel, "ReturnButton").gameObject, 
            (PointerEventData eventData) => Managers.Scene.LoadScene(Define.Scene.Main));

        _resultPanel.SetActive(true);
    }
}