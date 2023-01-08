using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageAbilityEffect : BaseAbilityEffect
{
    

    public override int Predict (Tile target)
    {
        Unit attacker = GetComponentInParent<Unit>();
        Unit defender = target.content.GetComponent<Unit>();

        int attack = GetStat(attacker, defender, GetAttackNotification, 0);
        int power = GetStat(attacker, defender, GetPowerNotification, 0);
        int defense = GetStat(attacker, defender, GetDefenseNotification, 0);
        
        int damage = (attack+power) - defense;
        damage = Mathf.Max(damage, 1);

        damage = GetStat(attacker, defender, TweakDamageNotification, damage);
        damage = Mathf.Clamp(damage, minDamage, maxDamage);
        return damage;
    }

    protected override int OnApply(Tile target)
    {
        Unit defender = target.content.GetComponent<Unit>();

        // Start with the predicted damage value
        int value = Predict(target);

        // Add some random variance
        value = Mathf.FloorToInt(value * UnityEngine.Random.Range(0.9f, 1.1f));

        // Clamp the damage to a range
        value = Mathf.Clamp(value, minDamage, maxDamage);

        // Apply the damage to the target
        Stats s = defender.GetComponent<Stats>();
        s[StatTypes.HP] += value;
        return value;
    }

    //public override void Apply(Tile target)
    //{
    //    Unit defender = target.content.GetComponent<Unit>();
    //    int value = Predict(target);
    //    value = Mathf.Clamp(value, minDamage, maxDamage);
    //    Stats s = defender.GetComponent<Stats>();
    //    s[StatTypes.HP] -= value;
    //}

    //int GetStat (Unit attacker, Unit target, string notification, int startValue)
    //{
    //    var mods = new List<ValueModifier>();
    //    var info = new Info<Unit, Unit, List<ValueModifier>>(attacker, target, mods);
    //    this.PostNotification(notification, info);
    //    mods.Sort();

    //    float value = startValue;
    //    for (int i = 0; info < mods.Count; ++i)
    //        value = mods[i].Modify(startValue, value);
    //    int retValue = Mathf.FloorToInt(value);
    //    retValue = Mathf.Clamp(retValue, minDamage, maxDamage);
    //    return retValue;
    //}
}
