using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfirmAbilityTargetState : BattleState
{
    List<Tile> tiles;
    AbilityArea aa;
    int index = 0;

    public override void Enter()
    {
        base.Enter();
        aa = turn.ability.GetComponent<AbilityArea>();
        tiles = aa.GetTilesInArea(board, pos);
        board.SelectTiles(tiles);
        FindTargets();
        RefreshPrimaryStatPanel(turn.actor.tile.pos);
        if (turn.targets.Count > 0)
        {
            SetTarget(0);
        }
        if (driver.Current == Drivers.Computer)
            StartCoroutine(ComputerDisplayAbilitySelection());
    }

    public override void Exit()
    {
        base.Exit();
        board.DeSelectTiles(tiles);
        statPanelController.HidePrimary();
        statPanelController.HideSecondary();
        
    }

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        if (e.info.y > 0 || e.info.x > 0)
            SetTarget(index + 1);
        else
            SetTarget(index - 1);
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if (e.info == 0)
        {
            if (turn.targets.Count > 0)
            {
                owner.ChangeState<PerformAbilityState>();
            }
        }
        else
            owner.ChangeState<AbilityTargetState>();
    }

    void FindTargets()
    {
        turn.targets = new List<Tile>();
        for (int i = 0; i < tiles.Count; ++i)
            if (turn.ability.IsTarget(tiles[i]))
                turn.targets.Add(tiles[i]);
    }

    void SetTarget(int target)
    {
        index = target;
        if (index < 0)
            index = turn.targets.Count - 1;
        if (index >= turn.targets.Count)
            index = 0;

        if (turn.targets.Count > 0)
        {
            RefreshSecondaryStatPanel(turn.targets[index].pos);
            
        }
    }

    IEnumerator ComputerDisplayAbilitySelection()
    {
        //owner.battleMessageController.Display(turn.ability.name);
        yield return new WaitForSeconds(2f);
        owner.ChangeState<PerformAbilityState>();
    }
}