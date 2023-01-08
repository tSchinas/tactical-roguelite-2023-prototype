using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// for resurrection abilities. looks for entity with 0 HP
/// </summary>
public class KOdAbilityEffectTarget : AbilityEffectTarget
{
    public override bool IsTarget(Tile tile)
    {
        if (tile == null || tile.content == null)
            return false;
        Stats s = tile.content.GetComponent<Stats>();
        return s != null && s[StatTypes.HP] <= 0;
    }
}
