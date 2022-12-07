using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Main;

        Managers.UI.ShowScene<UI_StatWindow>();
        //Managers.UI.ShowPopup<UI_Conversation>();
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
