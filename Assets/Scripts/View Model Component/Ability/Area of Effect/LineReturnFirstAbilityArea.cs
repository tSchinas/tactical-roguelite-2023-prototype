using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//for when all valid tiles will be affected by the ability
public class LineReturnFirstAbilityArea: AbilityArea
{
    public override bool isSingleTarget => true;
    public override List<Tile> GetTilesInArea(Board board, Point pos)
    {
        AbilityRange range = GetComponent<AbilityRange>();
        return range.GetTilesInRange(board);
    }
}
