using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//for when all valid tiles will be affected by the ability
public class FullAbilityArea : AbilityArea
{
    public override List<Tile> GetTilesInArea(Board board, Point pos)
    {
        AbilityRange range = GetComponent<AbilityRange>();
        return range.GetTilesInRange(board);
    }
}
