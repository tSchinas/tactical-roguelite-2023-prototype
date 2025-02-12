using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategorySelectionState : BaseAbilityMenuState
{
    protected override void LoadMenu() //implement status ailments that prevent actions here
    {
        if (menuOptions == null)
            menuOptions = new List<string>();
        else
            menuOptions.Clear();

        menuTitle = "Actions";
        menuOptions.Add("Attack");
        

        AbilityCatalog catalog = turn.actor.GetComponentInChildren<AbilityCatalog>();
        for (int i = 0; i < catalog.CategoryCount(); ++i)
            menuOptions.Add(catalog.GetCategory(i).name);
        menuOptions.Add("Weapon Swap");
        PlayableUnit unit = turn.actor.GetComponent<PlayableUnit>();
        int count = 1;
        bool[] locks = new bool[count];
        if (unit != null)
        {
            for(int i = 0; i< count; ++i)
            { 
                if (unit.eqSubWeapon == null)
                {
                    locks[i] = true;
                }
            }
        }
        
        abilityMenuPanelController.Show(menuTitle, menuOptions);
        abilityMenuPanelController.SetLocked(2, locks[0]);
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
        if(abilityMenuPanelController.selection == 0)

            Attack();
        else if (abilityMenuPanelController.selection == 2)
        {
            owner.ChangeState<SwapWeaponState>();
        }
        else
            SetCategory(abilityMenuPanelController.selection - 1);
    }

    protected override void Cancel()
    {
        owner.ChangeState<CommandSelectionState>();
    }

    void Attack()
    {
        turn.ability = turn.actor.GetComponentInChildren<Ability>();
        owner.ChangeState<AbilityTargetState>();
    }

    void SetCategory (int index)
    {
        ActionSelectionState.category = index;
        owner.ChangeState<ActionSelectionState>();
    }
}
