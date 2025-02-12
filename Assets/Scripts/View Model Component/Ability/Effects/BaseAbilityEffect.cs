using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityEffect : MonoBehaviour
{
	protected const int minDamage = 0;
	protected const int maxDamage = 999;
	
	public const string GetAttackNotification = "DamageAbilityEffect.GetAttackNotification";
	public const string GetDefenseNotification = "DamageAbilityEffect.GetDefenseNotification";
	public const string GetPowerNotification = "DamageAbilityEffect.GetPowerNotification";
	public const string TweakDamageNotification = "DamageAbilityEffect.TweakDamageNotification";
	public const string MissedNotification = "BaseAbilityEffect.MissedNotification";
	public const string HitNotification = "BaseAbilityEffect.HitNotification";

    protected abstract int GetBaseAttack();
    protected abstract int GetBaseDefense(Unit target);
    protected abstract int GetPower();
    public abstract int Predict(Tile target);
    public void Apply(Tile target)
    {

        if (GetComponent<AbilityEffectTarget>().IsTarget(target) == true)
            this.PostNotification(HitNotification, OnApply(target));
        else
            this.PostNotification(MissedNotification);

    }

    protected abstract int OnApply(Tile target);

	protected virtual int GetStat(Unit attacker, Unit target, string notification, int startValue)
	{
		var mods = new List<ValueModifier>();
		var info = new Info<Unit, Unit, List<ValueModifier>>(attacker, target, mods);
		this.PostNotification(notification, info);
		mods.Sort(Compare);

		float value = startValue;
		for (int i = 0; i < mods.Count; ++i)
			value = mods[i].Modify(startValue, value);

		int retValue = Mathf.FloorToInt(value);
		retValue = Mathf.Clamp(retValue, minDamage, maxDamage);
		return retValue;
	}

	int Compare(ValueModifier x, ValueModifier y)
	{
		return x.sortOrder.CompareTo(y.sortOrder);
	}
}
