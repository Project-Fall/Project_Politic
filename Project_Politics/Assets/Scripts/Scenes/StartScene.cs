using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Start;

        Managers.UI.ShowScene<UI_Start>();
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
