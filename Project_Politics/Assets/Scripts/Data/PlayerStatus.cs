using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerStatus : MonoBehaviour
{
    private void Start()
    {
        Managers.UI.ShowScene<UI_StatWindow>();
    }
}
