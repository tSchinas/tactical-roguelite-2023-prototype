using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonedStatus : BaseStatusEffect
{
    Unit owner;
    void OnEnable()
    {
        owner = GetComponentInParent<Unit>();
        if (owner)
            this.AddObserver(OnNewTurn, TurnManager.TurnBeganNotification, owner);
    }
    void OnDisable()
    {
        this.RemoveObserver(OnNewTurn, TurnManager.TurnBeganNotification, owner);
    }
    void OnNewTurn(object sender, object args)
    {
        Stats s = GetComponentInParent<Stats>();
        int currentHP = s[StatTypes.HP];
        int reduce = 1;
        s.SetValue(StatTypes.HP, (currentHP - reduce), false);
    }
}
