using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleScene : BaseScene
{
    // 나중에 Main -> Battle Scene으로 넘어올 때 후보자를 받아오는 걸로 생각 중
    [SerializeField] private UI_BattleStart battleStartUI;
    [SerializeField] private UI_Battle battleUI;
    public BattleController battleController;

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

    protected override void Init()
    {
        base.Init();
        battleController = new BattleController();
        battleStartUI = Managers.UI.ShowScene<UI_BattleStart>();
        battleStartUI.ButtonAction = (PointerEventData) => BattleStart(PointerEventData);
    }
    
    private void BattleStart(PointerEventData eventData)
    {
        battleStartUI.gameObject.SetActive(false);
        battleUI = Managers.UI.ShowScene<UI_Battle>();
    }
}
