using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance // Sington 나중에 수정해도 ㄱㅊ
    {
        get
        {
            if (Instance == null)
            {
                instance = FindObjectOfType<SkinManager>();
                if (Instance == null)
                {
                    var instanceContainer = new GameObject("SkinManager");
                    instance = instanceContainer.AddComponent<SkinManager>();
                }
            }
            return Instance;
        }
    }

    private static SkinManager instance;

    [System.Serializable]
    public class HeadAnimArray
    {
        public AnimationClip[] AnimClips;
    }
    public HeadAnimArray[] HeadArray;

    [System.Serializable]
    public class EyesAnimArray
    {
        public AnimationClip[] AnimClips;
    }
    public EyesAnimArray[] EyesArray;

    [System.Serializable]
    public class BodyAnimArray
    {
        public AnimationClip[] AnimClips;
    }
    public BodyAnimArray[] BodyArray;

}
