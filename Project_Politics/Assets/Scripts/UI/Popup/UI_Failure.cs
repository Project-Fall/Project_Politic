using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Failure : UI_Popup
{
    enum Objects
    {
        Background,
        GameOver,
        Reset,
        AD,
    }

    void Start()
    {
        Bind<GameObject>(typeof(Objects));

        BindEvent(GetObject((int)Objects.Reset), (PointerEventData) => OnClickReset());
        BindEvent(GetObject((int)Objects.AD), (PointerEventData) => OnClickAD());

        Init();
        StartCoroutine(ShowFailure());
    }

    private void Init()
    {
        GetObject((int)Objects.GameOver).SetActive(false);
        GetObject((int)Objects.Reset).SetActive(false);
        GetObject((int)Objects.AD).SetActive(false);
    }

    private void OnClickReset()
    {
        Managers.Data.GameData.Reset();
        Managers.Scene.LoadScene(Define.Scene.Start);
    }

    private void OnClickAD()
    {
        Debug.Log("AD");
    }

    private IEnumerator ShowFailure()
    {
        yield return new WaitForSeconds(3f);

        Image image = GetObject((int)Objects.Background).GetComponent<Image>();
        Color color = image.color;
        while(image.color.a < 1f)
        {
            color.a += 0.2f;
            image.color = color;
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1f);
        GetObject((int)Objects.GameOver).SetActive(true);

        yield return new WaitForSeconds(1f);
        GetObject((int)Objects.Reset).SetActive(true);
        GetObject((int)Objects.AD).SetActive(true);

    }
}
