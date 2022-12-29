using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SecretaryInfo : UI_Sub
{
    private Secretary _secretary;

    enum Objects
    {
        Image,
        Name,
        Stat,
    }

    private void Start()
    {
        Bind<GameObject>(typeof(Objects));
    }

    public void Init(Secretary secretary)
    {
        _secretary = secretary;
        GetObject((int)Objects.Image).GetComponent<Animator>().runtimeAnimatorController = secretary.Motion;
        GetObject((int)Objects.Image).GetComponent<Animator>().Play("Default");
        GetObject((int)Objects.Name).GetComponent<Text>().text = secretary.Name;
        GetObject((int)Objects.Stat).GetComponent<Text>();
    }
}
