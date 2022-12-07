using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Deploy : UI_Scene
{
    public class Item
    {
        string name;
        Image image;
    }

    enum Objects
    {
        Items
    }

    public List<Sprite> items = new List<Sprite>();

    private void Start()
    {
        Bind<GameObject>(typeof(Objects));
        items.Add(Managers.Resource.Load<Sprite>("Image/Sofa"));
        items.Add(Managers.Resource.Load<Sprite>("Image/Chair"));

        foreach (Sprite item in items)
        {
            GameObject go = Managers.Resource.Instantiate("UI/Item", GetObject(0).transform);
            go.GetComponent<Image>().sprite = item;
        }
    }
}