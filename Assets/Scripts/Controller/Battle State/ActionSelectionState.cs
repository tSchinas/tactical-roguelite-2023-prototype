using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class ActionSelectionState : BaseAbilityMenuState
{
    public static int category;
    AbilityCatalog catalog;

    protected override void LoadMenu()
    {
        catalog = turn.actor.GetComponentInChildren<AbilityCatalog>();
        GameObject container = catalog.GetCategory(category);
        menuTitle = container.name;

        int count = catalog.AbilityCount(container);
        if (menuOptions == null)
            menuOptions = new List<string>(count);
        else
            menuOptions.Clear();

        bool[] locks = new bool[count];
        for (int i = 0; i < count; ++i)
        {
            Ability ability = catalog.GetAbility(category, i);
            AbilityAPCost cost = ability.GetComponent<AbilityAPCost>();
            WeaponTypes curWep = turn.actor.GetComponent<PlayableUnit>().eqMainWeapon.type;
            int lvlReq = ability.GetComponent<LevelRequirement>().requiredLevel;
            int? curLvl = turn.actor.GetComponent<WeaponProficiency>().GetCurrentLevel(curWep);
            if (lvlReq <= curLvl)
            {
                if (cost)
                {
                    menuOptions.Add(string.Format("{0}: {1} AP", ability.name, cost.amount));
                }
                else if (!ability.isPassive)
                    menuOptions.Add(ability.name);
            }
            
            locks[i] = !ability.CanPerform();
        }

        abilityMenuPanelController.Show(menuTitle, menuOptions);
        for (int i = 0; i < count; ++i)
            abilityMenuPanelController.SetLocked(i, locks[i]);
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
        turn.ability = catalog.GetAbility(category, abilityMenuPanelController.selection);
        owner.ChangeState<AbilityTargetState>();
    }

    protected override void Cancel()
    {
        owner.ChangeState<CategorySelectionState>();
    }

    //void SetOptions(string[])
    //{
    //    //menuOptions.Clear();
    //    //for (int i = 0; i< options.Length; ++i)
    //    //    menuOptions.Add(options[i]);
    //}
}
