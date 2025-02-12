using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyStatPassiveEffect : BaseAbilityEffect
{
    public StatTypes statType;
    public int amount;
    private Unit owner;
    private void Start()
    {
        owner = GetComponentInParent<Unit>();
        if (owner == null)
            Debug.LogError("ModifyStatPassiveEffect: No Unit component found");
        IncreaseStat(statType, amount, owner);
    }
    private void OnDisable()
    {
        owner = GetComponentInParent<Unit>();
        DecreaseStat(statType, amount, owner);
    }
    public override int Predict(Tile target)
    {
        throw new System.NotImplementedException();
    }

    protected override int GetBaseAttack()
    {
        throw new System.NotImplementedException();
    }

    protected override int GetBaseDefense(Unit target)
    {
        throw new System.NotImplementedException();
    }

    protected override int GetPower()
    {
        throw new System.NotImplementedException();
    }

    protected override int OnApply(Tile target)
    {
        throw new System.NotImplementedException();
    }

    private void IncreaseStat(StatTypes type, int value, Unit unit)
    {
        Stats stats = unit.GetComponent<Stats>();
        stats[type] += value;
    }
    private void DecreaseStat(StatTypes type, int value, Unit unit)
    {
        Stats stats = unit.GetComponent<Stats>();
        stats[type] -= value;
    }
}
