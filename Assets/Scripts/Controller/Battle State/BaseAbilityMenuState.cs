using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityMenuState : BattleState //abstract base class so that I only need to implement the parts of the code which are different
{
    protected string menuTitle;
    protected List<string> menuOptions;

    public override void Enter()
    {
        base.Enter();
        //Debug.Log($"Entering state: {this.GetType().Name}");
        SelectTile(turn.actor.tile.pos);
        if (driver.Current == Drivers.Human)
            LoadMenu();
    }

    public override void Exit()
    {
        base.Exit();
        abilityMenuPanelController.Hide();
    }

    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        Debug.Log(e.info);
        if (e.info == 0)
            Confirm();
        else
            Cancel();
    }

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        if (e.info.x > 0 || e.info.y < 0)
            abilityMenuPanelController.Next();
        else
            abilityMenuPanelController.Previous();
    }

    protected abstract void LoadMenu();
    protected abstract void Confirm();
    protected abstract void Cancel();
}
