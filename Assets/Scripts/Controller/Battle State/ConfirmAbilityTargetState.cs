using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// once user has selected direction for range or location within range, enter this state.
/// potentially highlights new set of tile that shows AOE
/// </summary>
public class ConfirmAbilityTargetState : BattleState
{
    //AbilityEffectTarget[] listTargets;
    List<Tile> tiles;
    AbilityArea abilityArea;
    int index = 0;
    /// <summary>
    /// Enter
    /// looks through effect targeting components attached to ability and determines valid targets
    /// targets will show in secondary stat panel
    /// movement input cycles through targets displayed in stat panel
    /// </summary>
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered confirm ability target state.");
        abilityArea = turn.ability.GetComponent<AbilityArea>();
        tiles = abilityArea.GetTilesInArea(board, pos);
        board.SelectTiles(tiles);
        FindTargets();
        RefreshPrimaryStatPanel(turn.actor.tile.pos);
        SetTarget(0);
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
        Debug.Log("Confirm target event fired.");
        if (e.info == 0)
        {
            Debug.Log("e.info == 0 is true");
            if (turn.targets.Count > 0)
            {
                Debug.Log("valid targets are > 0");
                Debug.Log("Confirmed. Switching to perform ability state...");
                owner.ChangeState<PerformAbilityState>();
            }
        }
        else
            owner.ChangeState<AbilityTargetState>();
    }
    void FindTargets()
    {
        Debug.Log("Finding targets...");
        turn.targets = new List<Tile>();
        AbilityEffectTarget[] targeters = turn.ability.GetComponentsInChildren<AbilityEffectTarget>();
        for (int i = 0; i < tiles.Count; ++i)
            if (IsTarget(tiles[i], targeters))
                turn.targets.Add(tiles[i]);
                
    }

    bool IsTarget(Tile tile, AbilityEffectTarget[] list)
    {
        for (int i = 0; i < list.Length; ++i)
        {
            if (list[i].IsTarget(tile))
            {
                return true;
            }
            
        }
        return false;
    }
    void SetTarget(int target)
    {
        Debug.Log("Target set.");
        index = target;
        if (index < 0)
            index = turn.targets.Count - 1;
        if (index >= turn.targets.Count)
            index = 0;
        if (turn.targets.Count > 0)
            RefreshSecondaryStatPanel(turn.targets[index].pos);
    }
}
