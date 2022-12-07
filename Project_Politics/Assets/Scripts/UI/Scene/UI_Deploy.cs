using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UI_Deploy : UI_Scene
{
    public class Item
    {
        public string name;
        public Sprite image;
    }

    enum Objects
    {
        Items
    }

    public List<Item> items = new List<Item>();

    private void Start()
    {
        Bind<GameObject>(typeof(Objects));
        Init();
    }

    private void Init()
    {
        // 나중에 데이터에서 관리
        string pattern = "*.png";
        string[] names = Directory.GetFiles("Assets/Resources/Art/Sprites/Office_Object/Furniture", pattern);
        foreach (string n in names)
        {
            string name = Path.GetFileNameWithoutExtension(n);
            Item item = new Item();
            item.image = Managers.Resource.Load<Sprite>($"Art/Sprites/Office_Object/Furniture/{name}");
            if (item.image == null)
                continue;

            item.name = name;
            items.Add(item);
            GameObject go = Managers.Resource.Instantiate("UI/Item", GetObject(0).transform);
            go.GetComponent<Image>().sprite = item.image;
        }
    }
}