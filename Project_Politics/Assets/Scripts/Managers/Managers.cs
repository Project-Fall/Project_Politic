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

    private SceneManagerEX _scene = new SceneManagerEX();
    public static SceneManagerEX Scene { get { return Instance._scene; } }

    private SoundManager _sound = new SoundManager();
    public static SoundManager Sound { get { return Instance._sound; } }
    
    private DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }

    private InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }




    private static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@GameManager");
            
            if(go == null)
            {
                go = new GameObject("@GameManager");
                go.AddComponent<Managers>();
            }

            //DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}
