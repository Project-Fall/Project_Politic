using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    /// <summary>
    /// 하위 오브젝트 중 특정 이름 혹은 컴포넌트를 가진 오브젝트를 찾습니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <param name="name"></param>
    /// <param name="reculsive"></param>
    /// <returns></returns>
    public static T FindChild<T>(GameObject go, string name = null ,bool reculsive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        // 바로 아래 자식들만 검사
        if (reculsive == false)
        {
            for(int i=0; i<go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        } 

        // 최하위 자식까지 모두 검사
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }

        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool reculsive = false)
    {
        Transform transform = FindChild<Transform>(go, name, reculsive);
        if (transform == null)
            return null;
        return transform.gameObject;
    }
}
