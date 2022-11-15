using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpButton : MonoBehaviour
{
    int num;
    PlayerInfo info;

    public void Init(int n, PlayerInfo pi)
    {
        num = n;
        info = pi;
        GetComponent<Button>().onClick.AddListener(OnClickButton);
    }

    public void OnClickButton()
    {
        info.updateScore(num);
    }
}
