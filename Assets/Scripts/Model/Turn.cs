using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    public Unit actor;
    public GameObject ability;
    public List<Tile> targets;
    public bool hasUnitMoved;
    public bool hasUnitActed;
    public bool lockMove;
    Tile startTile;
    Directions startDir;

    public void Change (Unit current)
    {
        actor = current;
        hasUnitMoved = false;
        hasUnitActed = false;
        lockMove = false;
        startTile = actor.tile;
        startDir = actor.dir;
    }

    public void UndoMove()
    {
        hasUnitMoved = false;
        actor.Place(startTile);
        actor.dir = startDir;
        actor.Match();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
