using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFacingState : BattleState
{
    Directions startDir;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered end facing state.");
        startDir = turn.actor.dir;
        SelectTile(turn.actor.tile.pos);
        owner.facingIndicator.gameObject.SetActive(true);
        owner.facingIndicator.SetDirection(turn.actor.dir);
        if (driver.Current == Drivers.Computer)
            StartCoroutine(ComputerControl());
    }

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        turn.actor.dir = e.info.GetDirection();
        turn.actor.Match();
        owner.facingIndicator.SetDirection(turn.actor.dir);
    }
    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        switch (e.info)
        {
            case 0:
                owner.ChangeState<SelectUnitState>();
                owner.facingIndicator.gameObject.SetActive(false);
                break;
            case 1:
                turn.actor.dir = startDir;
                turn.actor.Match();
                owner.facingIndicator.gameObject.SetActive(false);
                owner.ChangeState<CommandSelectionState>();
                break;
        }
    }
    IEnumerator ComputerControl()
    {
        yield return new WaitForSeconds(0.5f);
        turn.actor.dir = owner.cpu.DetermineEndFacingDirection();
        turn.actor.Match();
        owner.facingIndicator.SetDirection(turn.actor.dir);
        yield return new WaitForSeconds(0.5f);
        owner.facingIndicator.gameObject.SetActive(false);
        owner.ChangeState<SelectUnitState>();
    }
}
