using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Gacha : UI_Popup
{
    enum Objects
    {
        EnvelopImage,
        Back,
        GachaButton,
        Resume,
        White,
        Price,
        Money,
    }

    [SerializeField] private int GachaPrice = 20;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        Bind<GameObject>(typeof(Objects));

        BindEvent(GetObject((int)Objects.Back), (PointerEventData) => Managers.UI.ClosePopup(this));
        BindEvent(GetObject((int)Objects.GachaButton), (PointerEventData) => StartCoroutine(Gacha()));
        GetObject((int)Objects.Resume).SetActive(false);
        GetObject((int)Objects.Price).GetComponent<Text>().text = $"재화 {GachaPrice} 소모";
        GachaAvailable();
        SetMoneyText();
    }

    private IEnumerator Gacha()
    {
        SetResume();
        SetMoneyText(GachaPrice);
        GetObject((int)Objects.Resume).SetActive(false);
        GetObject((int)Objects.GachaButton).SetActive(false);
        GetObject((int)Objects.Back).SetActive(false);
        GetObject((int)Objects.EnvelopImage).SetActive(true);
        GetObject((int)Objects.White).SetActive(true);
        GetComponent<Animator>().SetTrigger("Open");

        yield return new WaitForSeconds(2f);
        GetObject((int)Objects.EnvelopImage).SetActive(false);
        GetObject((int)Objects.White).SetActive(false);
        GetObject((int)Objects.Resume).SetActive(true);
        GetComponent<Animator>().SetTrigger("Exit");

        yield return new WaitForSeconds(0.2f);
        GetObject((int)Objects.Back).SetActive(true);
        GachaAvailable();
        GetObject((int)Objects.GachaButton).SetActive(true);
    }

    private Secretary GetNewSecretary()
    {
        return Managers.Data.Secretarys[UnityEngine.Random.Range(0, Managers.Data.Secretarys.Count)];
    }

    private void SetResume()
    {
        Secretary newSec = GetNewSecretary();
        Managers.Data.GameData.AddSecretary(newSec);
        GameObject resume = GetObject((int)Objects.Resume);
        GameObject image = Util.FindChild(resume, "Image");
        image.GetComponent<Animator>().runtimeAnimatorController = newSec.Motion;
        image.GetComponent<Image>().sprite = image.GetComponent<SpriteRenderer>().sprite;
        Util.FindChild(resume, "Name").GetComponent<Text>().text = newSec.Name;
        Util.FindChild(resume, "Stats").GetComponent<Text>().text = MakeStatText(newSec);
    }

    private void SetMoneyText(int money = 0)
    {
        GetObject((int)Objects.Money).GetComponent<Text>().text = $"재화 : {Managers.Data.GameData.SetMoney(-money)}";
    }

    private bool GachaAvailable()
    {
        if (Managers.Data.GameData.GetMoney() < GachaPrice)
        {
            GetObject((int)Objects.GachaButton).GetComponent<Button>().interactable = false;
            return false;
        }
        else
        {
            GetObject((int)Objects.GachaButton).GetComponent<Button>().interactable = true;
            return true;
        }
    }

    private string MakeStatText(Secretary secretary)
    {
        string text = "";
        for(int i = 0; i<secretary.Stat.Length; i++)
        {
            if(secretary.Stat[i] != 0)
            {
                text += $"{Enum.GetName(typeof(Define.StatKor), i)} : {secretary.Stat[i]}\n";
            }
        }
        return text;
    }
}
