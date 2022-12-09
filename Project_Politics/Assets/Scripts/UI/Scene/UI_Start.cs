using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Start : UI_Scene
{
    enum Buttons
    {
        Start,
        Option,
        Exit,
    }

    void Start()
    {
        Bind<Button>(typeof(Buttons));

        BindEvent(GetButton((int)Buttons.Start).gameObject, (PointerEventData) => Managers.Scene.LoadScene(Define.Scene.Main));
        BindEvent(GetButton((int)Buttons.Option).gameObject, (PointerEventData) => Managers.UI.ShowPopup<UI_Option>());
        BindEvent(GetButton((int)Buttons.Exit).gameObject, (PointerEventData) => ExitGame());
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
