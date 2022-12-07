using System.Collections;
using UnityEngine;

public class TransformEffect : MonoBehaviour
{
    public Vector2 Direction;
    private Vector2 _startPos;
    private Vector2 _goalPos;

    void Start()
    {
        _startPos = transform.position;
        _goalPos = _startPos - new Vector2(0, -360);
    }

    void Update()
    {
        Vector2.MoveTowards(transform.position, _goalPos, Time.deltaTime * 50);
    }

    public void ToGoal()
    {

    }

    public void ToReturn()
    {

    }
}
