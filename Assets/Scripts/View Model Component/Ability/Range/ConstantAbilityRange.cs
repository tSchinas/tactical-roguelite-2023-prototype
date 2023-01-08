using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used when an ability always has a predetermined amount for range i.e Spell X has range Y.
public class ConstantAbilityRange : AbilityRange
{
    //similar to Movement component using board's search ability to retrieve list of tiles within range of user
    public override List<Tile> GetTilesInRange(Board board)
    {
        return board.Search(unit.tile, ExpandSearch);
    }
    //limits search to continue only as long as within range specified by horizontal/vertical
    bool ExpandSearch(Tile from, Tile to)
    {
        return (from.distance + 1) <= horizontal && Mathf.Abs(to.height - unit.tile.height) <= vertical;
    }
}
