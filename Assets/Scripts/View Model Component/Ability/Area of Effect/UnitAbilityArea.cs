using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//for when area is a single unit/tile
public class UnitAbilityArea : AbilityArea
{
    public override List<Tile> GetTilesInArea(Board board, Point pos)
    {
        List<Tile> retValue = new();
        Tile tile = board.GetTile(pos);
        if (tile != null)
            retValue.Add(tile);
        return retValue;
    }
}
