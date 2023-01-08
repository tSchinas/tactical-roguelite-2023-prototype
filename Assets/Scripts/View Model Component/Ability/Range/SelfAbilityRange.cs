using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for abilities that only affect the user; no need to search so only returns user's tile
public class SelfAbilityRange : AbilityRange
{
    public override List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = new List<Tile>(1);
        retValue.Add(unit.tile);
        return retValue;
    }
}
