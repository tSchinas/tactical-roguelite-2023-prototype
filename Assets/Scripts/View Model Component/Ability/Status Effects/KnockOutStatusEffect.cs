using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockOutStatusEffect : BaseStatusEffect
{
    Unit owner;
    Stats stats;

    private void Awake()
    {
        owner = GetComponentInParent<Unit>();
        stats = owner.GetComponent<Stats>();
    }
    private void OnEnable()
    {
        owner.transform.localScale = new Vector3(0.75f, 0.1f, 0.75f);
        this.AddObserver(OnTurnCheck, TurnManager.TurnCheckNotification, owner);
        //this.AddObserver(OnStatCounterWillChange, Stats.WillChangeNotification(StatTypes.CTR), stats);
    }
    void OnDisable()
    {
        owner.transform.localScale = Vector3.one;
        this.RemoveObserver(OnTurnCheck, TurnManager.TurnCheckNotification, owner);
        //this.RemoveObserver(OnStatCounterWillChange, Stats.WillChangeNotification(StatTypes.CTR), stats);
    }

    void OnTurnCheck(object sender, object args)
    {
        // Dont allow a KO'd unit to take turns
        BaseException exc = args as BaseException;
        if (exc.defaultToggle == true)
            exc.FlipToggle();
    }
    void OnStatCounterWillChange(object sender, object args)
    {
        // Dont allow a KO'd unit to increment the turn order counter
        ValueChangeException exc = args as ValueChangeException;
        if (exc.toValue > exc.fromValue)
            exc.FlipToggle();
    }
}
