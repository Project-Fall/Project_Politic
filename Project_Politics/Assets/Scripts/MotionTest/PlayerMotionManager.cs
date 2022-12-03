using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionManager : MonoBehaviour
{
    public static PlayerMotionManager Instance // Sington 나중에 수정해도 ㄱㅊ
    {
        get
        {
            if (Instance == null)
            {
                instance = FindObjectOfType<PlayerMotionManager>();
                if(Instance == null)
                {
                    var instanceContainer = new GameObject("PlayerMotionManager");
                    instance = instanceContainer.AddComponent<PlayerMotionManager>();
                }
            }
            return Instance;
        }
    }

    private static PlayerMotionManager instance;

    public PlayerSkin[] PlayerSkins;

    public void SkinSetTrigger(string AniName)
    {
        foreach(PlayerSkin skin in PlayerSkins)
        {
            skin.anim.SetTrigger(AniName);
        }
    }



}
