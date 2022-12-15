using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    public GameObject Work;
    public Secretary secretary;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Main;

        Managers.UI.ShowScene<UI_Main>();

        Managers.Sound.Play("BGM/Main", Define.Sound.Bgm);
        Work.GetComponent<Animator>().runtimeAnimatorController = secretary.Motion;
        Work.GetComponent<Animator>().Play("Work");
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
