using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageAbilityEffect : BaseAbilityEffect
{
    

    private void OnEnable()
    {
        this.AddObserver(OnGetBaseAttack, BaseAbilityEffect.GetAttackNotification);
        this.AddObserver(OnGetBaseDefense, BaseAbilityEffect.GetDefenseNotification);
        this.AddObserver(OnGetPower, BaseAbilityEffect.GetPowerNotification);
    }

    

    private void OnDisable()
    {
        this.RemoveObserver(OnGetBaseAttack, BaseAbilityEffect.GetAttackNotification);
        this.RemoveObserver(OnGetBaseDefense, BaseAbilityEffect.GetDefenseNotification);
        this.RemoveObserver(OnGetPower, BaseAbilityEffect.GetPowerNotification);
    }

    private void OnGetBaseAttack(object sender, object args)
    {
        if(IsMyEffect(sender))
        {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add(new AddValueModifier(0, GetBaseAttack()));
        }
    }

    void OnGetBaseDefense(object sender, object args)
    {
        if (IsMyEffect(sender))
        {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add(new AddValueModifier(0, GetBaseDefense(info.arg1)));
        }
    }

    void OnGetPower(object sender, object args)
    {
        if (IsMyEffect(sender))
        {
            var info = args as Info<Unit, Unit, List<ValueModifier>>;
            info.arg2.Add(new AddValueModifier(0, GetPower()));
        }
    }
    bool IsMyEffect(object sender)
    {
        MonoBehaviour obj = sender as MonoBehaviour;
        return (obj != null && obj.transform.parent == transform);
    }
    public override int Predict (Tile target)
    {
        Unit attacker = GetComponentInParent<Unit>();
        Unit defender = target.content.GetComponent<Unit>();

        

        //int abilityPower = 

        int attack = GetStat(attacker, defender, GetAttackNotification, 0);
        int power = GetStat(attacker, defender, GetPowerNotification, 0);
        int defense = GetStat(attacker, defender, GetDefenseNotification, 0);
        
        int damage = (attack+power+1) - defense;
        damage = Mathf.Max(damage, 0);

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
        //value = Mathf.FloorToInt(value * UnityEngine.Random.Range(0.9f, 1.1f));

        // Clamp the damage to a range
        value = Mathf.Clamp(value, minDamage, maxDamage);

        // Apply the damage to the target
        Stats s = defender.GetComponent<Stats>();
        s[StatTypes.HP] -= value;
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
    #region private
    int GetStat(Unit attacker, Unit target, string notification, int startValue)
    {
        var mods = new List<ValueModifier>();
        var info = new Info<Unit, Unit, List<ValueModifier>>(attacker, target, mods);
        this.PostNotification(notification, info);
        mods.Sort();

        float value = startValue;
        for (int i = 0; i < mods.Count; ++i)
            value = mods[i].Modify(startValue, value);
        int retValue = Mathf.FloorToInt(value);
        retValue = Mathf.Clamp(retValue, minDamage, maxDamage);
        return retValue;
    }

    protected override int GetBaseAttack()
    {
        return GetComponentInParent<Stats>()[StatTypes.ATK];
    }

    protected override int GetBaseDefense(Unit target)
    {
        return GetComponentInParent<Stats>()[StatTypes.DEF];
    }

    protected override int GetPower()
    {
        throw new NotImplementedException();
    }
    #endregion
}
