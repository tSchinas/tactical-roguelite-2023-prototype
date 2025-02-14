using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityRange : MonoBehaviour
{
    public int horizontal = 1;//defines # of tiles away from user which can be reached
    public int vertical = int.MaxValue;//defines height difference between user's tile and target tiles within reach
    public virtual bool positionOriented { get { return true; } }
    public virtual bool DirectionOriented { get { return false; } }//should be true when range is a pattern i.e cone/line. when true, use movement input buttons to change facing. when false, move cursor to select tile in range
    protected Unit unit { get { return GetComponentInParent<Unit>(); } }//crawls through hierarchy to find Unit component
    public abstract List<Tile> GetTilesInRange(Board board);//returns list of tiles that can be reached by selected ability to highlight on board

    public virtual bool returnFirstInLine { get { return false; } }

    
    
    
}
