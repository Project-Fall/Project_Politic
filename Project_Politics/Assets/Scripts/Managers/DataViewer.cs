using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataViewer : MonoBehaviour
{
    [SerializeField] List<Character> NPCs;
    [SerializeField] List<Event> Events;
    [SerializeField] List<Secretary> Secretarys;

    private void Start()
    {
        NPCs = Managers.Data.NPCs;
        Events = Managers.Data.Events;
        Secretarys = Managers.Data.Secretarys;
    }
}
