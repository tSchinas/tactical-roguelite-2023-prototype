using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Differentiates between North/South and East/West axes.
//When unit faces North, outer loop increments on Y for as many tiles as specified by horizontal
//Then, inner loop on X begins with lateral offset of 1 and increments by 2 to get odd numbers for sideways spread of cone at each step from user
//horizontal reach iterates over Y because Point class used to represent tile's position uses X/Y, but is checking the Z axis
public class ConeAbilityRange : AbilityRange
{
    public override bool DirectionOriented { get { return true; } }
    public override List<Tile> GetTilesInRange(Board board)
    {
        Point pos = unit.tile.pos;
        List<Tile> retValue = new();
        int dir = (unit.dir == Directions.North || unit.dir == Directions.East) ? 1 : -1;
        int lateral = 1;

        if (unit.dir==Directions.North||unit.dir==Directions.South)
        {
            for (int y = 1; y <= horizontal; ++y)
            {
                int min = -(lateral / 2);
                int max = (lateral / 2);
                for (int x = min;x<=max;++x)
                {
                    Point next = new Point(pos.x + x, pos.y + (y * dir));
                    Tile tile = board.GetTile(next);
                    if (ValidTile(tile))
                        retValue.Add(tile);
                }
                lateral += 2;
            }
        }
        else
        {
            for (int x = 1; x<=horizontal;++x)
            {
                int min = -(lateral / 2);
                int max = (lateral / 2);
                for (int y = min; y <=max; ++y)
                {
                    Point next = new Point(pos.x + (x * dir), pos.y + y);
                    Tile tile = board.GetTile(next);
                    if (ValidTile(tile))
                        retValue.Add(tile);
                }
                lateral += 2;
            }
        }
        return retValue;
    }

    bool ValidTile (Tile t)
    {
        return t != null && Mathf.Abs(t.height - unit.tile.height) <= vertical;
    }
}
