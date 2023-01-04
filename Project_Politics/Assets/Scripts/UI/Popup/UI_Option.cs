using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Option : UI_Popup
{
    enum Objects
    {
        ClickBlocker,
        CloseButton,
        BGMSlider,
        SESlider,
    }
    
    void Start()
    {
        Bind<GameObject>(typeof(Objects));

        // X 버튼을 누르거나, 창 바깥을 누를 때 꺼짐
        BindEvent(GetObject((int)Objects.ClickBlocker), (PointerEventData) => Close());
        BindEvent(GetObject((int)Objects.CloseButton), (PointerEventData) => Close());

        GetObject((int)Objects.BGMSlider).GetComponent<Slider>().value = Managers.Data.GameData.BGMVolume;
        GetObject((int)Objects.SESlider).GetComponent<Slider>().value = Managers.Data.GameData.SEVolume;
        GetObject((int)Objects.BGMSlider).GetComponent<Slider>().onValueChanged.AddListener(ChangeBGMVolume);
        GetObject((int)Objects.SESlider).GetComponent<Slider>().onValueChanged.AddListener(ChangeSEVolume);
    }

    public void Close()
    {
        Managers.UI.ClosePopup(this);
    }

    public void ChangeBGMVolume(float value)
    {
        Managers.Data.GameData.BGMVolume = value;
        Managers.Sound.ChangeVolume(value);
    }

    public void ChangeSEVolume(float value)
    {
        Managers.Data.GameData.SEVolume = value;
    }
}
