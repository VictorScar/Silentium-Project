using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPointCounter
{
    int actionPointCount = 0;
    public ActionPointCounter(int startActionPoint)
    {
        ActionPointCount = startActionPoint;
    }

    public int ActionPointCount { get => actionPointCount; private set => actionPointCount = value; }


    public void SpendActionPoints(int points)
    {
        ActionPointCount -= points;
    }

    public void SetActionPoints(int points)
    {
        ActionPointCount = points;
    }

    public void GetActionPoints(int points)
    {
        ActionPointCount += points;
    }
}
