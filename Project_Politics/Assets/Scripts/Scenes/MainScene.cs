using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Main;

        Managers.UI.ShowScene<UI_Main>();

        Managers.Sound.Play("BGM/Main", Define.Sound.Bgm);
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
