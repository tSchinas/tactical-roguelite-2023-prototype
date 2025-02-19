using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatAllEnemiesVictoryCondition : BaseVictoryCondition
{
    protected override void CheckForGameOver()
    {
        base.CheckForGameOver();
        if (Victor == Alliances.None && PartyDefeated(Alliances.Enemy))
            Victor = Alliances.Hero;
        Debug.Log($"CheckForGameOver called. Victor: {Victor}");
    }

    public override void ResetVictor()
    {
        base.ResetVictor();
    }
}
