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
        TemporaryAttackExample();

        if (turn.hasUnitMoved)
            owner.ChangeState<EndFacingState>();
        else
            owner.ChangeState<CommandSelectionState>();
    }

    void TemporaryAttackExample()
    {
        for (int i = 0; i<turn.targets.Count;++i)
        {
            GameObject obj = turn.targets[i].content;
            Stats userStats = turn.actor.GetComponent<Stats>();
            Stats targetStats = obj != null ? obj.GetComponentInChildren<Stats>() : null;
            if (targetStats!=null)
            {
                targetStats[StatTypes.HP] -= (userStats[StatTypes.ATK]-targetStats[StatTypes.DEF]);
                if (targetStats[StatTypes.HP] <= 0)
                    Debug.Log("Unit KO'd'!", obj);
            }
        }
    }    
}
