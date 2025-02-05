using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageAbilityEffect : BaseAbilityEffect
{
    public int abilityPower = 0;
    public int hits = 1;

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

        //int attack = GetStat(attacker, defender, GetAttackNotification, 0);
        //int power = GetStat(attacker, defender, GetPowerNotification, 0);
        //int defense = GetStat(attacker, defender, GetDefenseNotification, 0);
        
        int attack = attacker.GetComponent<Stats>()[StatTypes.ATK];
        //int power = 0;
        int defense = defender.GetComponent<Stats>()[StatTypes.DEF];

        int damage = (attack+abilityPower) - defense;
        damage = Mathf.Max(damage, 0);

        //damage = GetStat(attacker, defender, TweakDamageNotification, damage);
        damage = Mathf.Clamp(damage, minDamage, maxDamage);
        return damage;
    }

    protected override int OnApply(Tile target)
    {
        
            Unit defender = target.content.GetComponent<Unit>();

            // Start with the predicted damage value
            int value = Predict(target);

            // Clamp the damage to a range
            value = Mathf.Clamp(value, minDamage, maxDamage);

            // Apply the damage to the target
            Stats s = defender.GetComponent<Stats>();
        for (int i = 0; i < hits; i++)
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
    protected override int GetStat(Unit attacker, Unit target, string notification, int startValue)
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
        int atk = GetComponentInParent<Stats>()[StatTypes.ATK];
        return atk;
    }

    protected override int GetBaseDefense(Unit target)
    {
        int def = GetComponentInParent<Stats>()[StatTypes.DEF];
        return def;
    }

    protected override int GetPower()
    {
        throw new NotImplementedException();
    }
    #endregion
}
