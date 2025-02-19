using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreBattlePreparationState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        Inventory inventory = FindFirstObjectByType<Inventory>();
        inventory.InitializeUI();
    }
    public override void Exit()
    {
        base.Exit();
        List<GameObject> objs = new();
        PartyMemberUI[] partyUis = FindObjectsOfType<PartyMemberUI>();

        foreach (var item in partyUis)
        {
            objs.Add(item.gameObject);
        }

        foreach (var item in objs)
        {
            Destroy(item);
        }
    }
}
