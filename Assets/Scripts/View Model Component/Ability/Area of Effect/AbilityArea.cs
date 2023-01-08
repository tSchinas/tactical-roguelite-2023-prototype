using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//returns a list of tiles in its area
//highlights candidate locations for effect application
public abstract class AbilityArea : MonoBehaviour
{
    public abstract List<Tile> GetTilesInArea(Board board, Point pos);//indicates selected location within a range from which to determine tiles to grab
}
