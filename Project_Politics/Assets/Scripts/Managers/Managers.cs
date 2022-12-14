using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_instance;
    private static Managers Instance { get { Init(); return s_instance; } }

    private ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance._resource; } }

    private UIManager _ui = new UIManager();
    public static UIManager UI { get { return Instance._ui; } }

    private static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("GameManager");
            
            if(go == null)
            {
                go = new GameObject("GameManager");
                go.AddComponent<Managers>();
            }

            //DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}
