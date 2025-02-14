using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//for when area is a single unit/tile
public class UnitAbilityArea : AbilityArea
{
    public override bool isSingleTarget => true;
    public override List<Tile> GetTilesInArea(Board board, Point pos)
    {
        List<Tile> retValue = new List<Tile>();
        Tile tile = board.GetTile(pos);
        if (tile != null)
            retValue.Add(tile);
        return retValue;
    }
}
