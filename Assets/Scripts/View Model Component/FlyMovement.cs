using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : Movement
{
    public override IEnumerator Traverse(Tile tile)
    {
        //store the distance between start and target tiles
        float dist = Mathf.Sqrt(Mathf.Pow(tile.pos.x - unit.tile.pos.x, 2) + Mathf.Pow(tile.pos.y - unit.tile.pos.y, 2));
        unit.Place(tile);

        //fly high enough not to clip through any ground tiles
        float y = Tile.stepHeight * 10;
        float duration = (y - jumper.position.y) * .5f;
        Tweener tweener = jumper.MoveToLocal(new Vector3(0, y, 0), duration, EasingEquations.EaseInOutQuad);
        while (tweener != null)
            yield return null;

        //turn to face general direction
        Directions dir;
        Vector3 toTile = (tile.Center - transform.position);
        if (Mathf.Abs(toTile.x) > Mathf.Abs(toTile.z))
            dir = toTile.x > 0 ? Directions.East : Directions.West;
        else
            dir = toTile.z > 0 ? Directions.North : Directions.South;
        yield return StartCoroutine(Turn(dir));

        //move to correct position
        duration = dist * .5f;
        tweener = transform.MoveTo(tile.Center, duration, EasingEquations.EaseInOutQuad);
        while (tweener != null)
            yield return null;

        //land
        duration = (y - tile.Center.y) * .5f;
        tweener = jumper.MoveToLocal(Vector3.zero, .5f, EasingEquations.EaseInOutQuad);
        while (tweener != null)
            yield return null;
    }
    
}
