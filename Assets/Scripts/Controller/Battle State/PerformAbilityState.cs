using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PerformAbilityState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Performing ability...");
        turn.hasUnitActed = true;
        if (turn.hasUnitMoved)
            turn.lockMove = true;
        StartCoroutine(Animate());
    }
    IEnumerator Animate()
    {
        //todo play animation, etc
        yield return null;
        //todo apply ability effect, etc
        ApplyAbility();
        StartCoroutine(ShowDamageAndExp(turn.ability));
        
        yield return new WaitForSeconds(2.5f);
        if (IsBattleOver())
            owner.ChangeState<EndBattleState>();
        else if (!UnitHasControl())
            owner.ChangeState<SelectUnitState>();
        else if (turn.hasUnitMoved)
            owner.ChangeState<EndFacingState>();
        else
            owner.ChangeState<CommandSelectionState>();
    }

    void ApplyAbility()
    {
        turn.ability.Perform(turn.targets);
        WeaponProficiency wp = turn.actor.GetComponent<WeaponProficiency>();
        if (wp)
        {
            wp.GainExperience(turn.actor.GetComponent<PlayableUnit>().eqMainWeapon, turn.actor.GetComponent<PlayableUnit>().eqSubWeapon);
        }
        //BaseAbilityEffect[] effects = turn.ability.GetComponentsInChildren<BaseAbilityEffect>();
        //for (int i = 0; i < turn.targets.Count; ++i)
        //{
        //    Tile target = turn.targets[i];
        //    for (int j = 0; j < effects.Length; ++j)
        //    {
        //        BaseAbilityEffect effect = effects[j];
        //        AbilityEffectTarget targeter = effect.GetComponent<AbilityEffectTarget>();
        //        if (targeter.IsTarget(target))
        //        {

        //            effect.Apply(target);
        //        }
        //    }
        //}
    }

    private IEnumerator ShowDamageAndExp(Ability ability)
    {
        Drivers cd = driver.Current;
        switch (cd)
        {
            case Drivers.Human:
                if (ability.GetComponentInChildren<DamageAbilityEffect>())
                {
                    for (int i = 0; i < turn.targets.Count; ++i)
                    {
                        GameObject dmgObj = Instantiate(owner.damageText, turn.targets[i].content.transform);
                        
                        TextMeshPro dmgText = dmgObj.GetComponentInChildren<TextMeshPro>();
                        if (turn.targets[i].content != null)
                        {
                            dmgText.text = turn.targets[i].content.GetComponent<Stats>().lastDamageTaken.ToString();
                        }
                    }
                }
                yield return new WaitForSeconds(1.5f);
                GameObject expObj = Instantiate(owner.expText, turn.actor.transform);
                TextMeshPro expText = expObj.GetComponentInChildren<TextMeshPro>();
                expText.text = turn.actor.GetComponent<WeaponProficiency>().MainGainDisplay;
                if (turn.actor.GetComponent<PlayableUnit>().eqSubWeapon)
                {
                    yield return new WaitForSeconds(1);
                    GameObject subExpObj = Instantiate(owner.expText, turn.actor.transform);
                    TextMeshPro subExpText = subExpObj.GetComponentInChildren<TextMeshPro>();
                    subExpText.text = turn.actor.GetComponent<WeaponProficiency>().SubGainDisplay;
                }
                
                break;
            case Drivers.Computer:
                if (ability.GetComponentInChildren<DamageAbilityEffect>())
                {
                    for (int i = 0; i < turn.targets.Count; ++i)
                    {
                        GameObject dmgObj = Instantiate(owner.damageText, turn.targets[i].content.transform);

                        TextMeshPro dmgText = dmgObj.GetComponentInChildren<TextMeshPro>();
                        if (turn.targets[i].content != null)
                        {
                            dmgText.text = turn.targets[i].content.GetComponent<Stats>().lastDamageTaken.ToString();
                        }
                    }
                }
                break;
        }
    }
    bool UnitHasControl()
    {
        return turn.actor.GetComponentInChildren<KnockOutStatusEffect>() == null;
    }
}
