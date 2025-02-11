using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapWeaponState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Swap());
    }

    IEnumerator Swap()
    {
        PlayableUnit u = turn.actor.GetComponent<PlayableUnit>();
        yield return StartCoroutine(u.WeaponSwap());
        owner.ChangeState<CommandSelectionState>();
    }
}
