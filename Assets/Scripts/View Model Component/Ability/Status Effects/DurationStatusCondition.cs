using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationStatusCondition : StatusCondition
{
    public int duration = 1;
    void OnEnable()
    {
        this.AddObserver(OnNewTurn, TurnManager.RoundBeganNotification);
    }
    void OnDisable()
    {
        this.RemoveObserver(OnNewTurn, TurnManager.RoundBeganNotification);
    }
    void OnNewTurn(object sender, object args)
    {
        duration--;
        if (duration <= 0)
            Remove();
    }
}
