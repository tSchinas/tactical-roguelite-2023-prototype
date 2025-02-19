using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        Debug.LogWarning("Battle ended!");
        SceneManager.LoadScene("VictoryScreen", LoadSceneMode.Additive);
        for (int i = 0; i < owner.enemyUnits.Count; ++i)
        {
            Destroy(owner.enemyUnits[i].gameObject);
        }
        owner.enemyUnits.Clear();
        for (int i = 0; i<owner.units.Count; ++i)
        {
            if (owner.units[i] = null)
                owner.units.RemoveAt(i);
        }

    }

    public override void Exit()
    {
        base.Exit();
        RandomDropUI[] uisToDestroy = FindObjectsOfType<RandomDropUI>();
        foreach (var item in uisToDestroy)
        {
            Destroy(item.gameObject);
        }
        SceneManager.UnloadSceneAsync("VictoryScreen");
    }
}
