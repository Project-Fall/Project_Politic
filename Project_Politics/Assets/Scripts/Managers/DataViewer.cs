using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataViewer : MonoBehaviour
{
    [SerializeField] List<Character> NPCs;
    [SerializeField] List<Event> Events;
    [SerializeField] List<Secretary> Secretarys;
    [SerializeField] List<Secretary> MySecretary;

    private void Start()
    {
        NPCs = Managers.Data.NPCs;
        Events = Managers.Data.Events;
        Secretarys = Managers.Data.Secretarys;
        MySecretary = Managers.Data.GameData.MySecretary;
    }
}
