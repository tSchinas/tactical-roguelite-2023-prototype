using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategorySelectionState : BaseAbilityMenuState
{
    protected override void LoadMenu() //implement status ailments that prevent actions here
    {
        if (menuOptions == null)
        {
            menuTitle = "Action";
            menuOptions = new List<string>(3);
            menuOptions.Add("Attack");
            menuOptions.Add("Black Magic");
            menuOptions.Add("White Magic");
        }
        abilityMenuPanelController.Show(menuTitle, menuOptions);
    }
    public override void Enter()
    {
        base.Enter();
        statPanelController.ShowPrimary(turn.actor.gameObject);
    }
    public override void Exit()
    {
        base.Exit();
        statPanelController.HidePrimary();
    }
    protected override void Confirm()
    {
        switch(abilityMenuPanelController.selection)
        {
            case 0:
                Attack();
                break;
            case 1:
                SetCategory(0);
                break;
            case 2:
                SetCategory(1);
                break;
        }
    }

    protected override void Cancel()
    {
        owner.ChangeState<CommandSelectionState>();
    }

    void Attack()
    {
        //turn.hasUnitActed = true;
        //if (turn.hasUnitMoved)
        //    turn.lockMove = true;
        //owner.ChangeState<CommandSelectionState>();
        turn.ability = turn.actor.GetComponentInChildren<Ability>();
        owner.ChangeState<AbilityTargetState>();
    }

    void SetCategory (int index)
    {
        ActionSelectionState.category = index;
        owner.ChangeState<ActionSelectionState>();
    }
}
