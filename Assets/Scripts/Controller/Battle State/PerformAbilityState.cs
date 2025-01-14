using System.Collections;
using System.Collections.Generic;
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

        if (turn.hasUnitMoved)
            owner.ChangeState<EndFacingState>();
        else
            owner.ChangeState<CommandSelectionState>();
    }

    void ApplyAbility()
    {
        BaseAbilityEffect[] effects = turn.ability.GetComponentsInChildren<BaseAbilityEffect>();
        for (int i = 0; i < turn.targets.Count; ++i)
        {
            Tile target = turn.targets[i];
            for (int j = 0; j < effects.Length; ++j)
            {
                BaseAbilityEffect effect = effects[j];
                AbilityEffectTarget targeter = effect.GetComponent<AbilityEffectTarget>();
                if (targeter.IsTarget(target))
                {
                    
                    effect.Apply(target);
                }
            }
        }
    }
}
