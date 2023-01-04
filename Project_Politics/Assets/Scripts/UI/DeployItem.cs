using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployItem : MonoBehaviour
{
    public Sprite Image;
    public bool isClick = false;

    //private void OnMouseDrag()
    //{
    //    Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    transform.localPosition = new Vector3(vector.x, vector.y, 0);
    //}

    private void Update()
    {
        if (isClick)
        {
            Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localPosition = new Vector3(vector.x, vector.y, 0);
        }
    }

    private void OnMouseDown()
    {
        isClick = true;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    private void OnMouseUp()
    {
        isClick = false;
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().sortingOrder = 0;
    }
}
