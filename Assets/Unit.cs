using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    public Tile tile { get; protected set; }
    public Directions dir;
    

    public void Place (Tile target)
    {
        //make sure old tile location is not still pointing to this unit
        if (tile != null && tile.content == gameObject)
            tile.content = null;

        //link tile and tile references
        tile = target;

        if (target != null)
            target.content = gameObject;
    }

    public void Match()
    {
        transform.localPosition = tile.Center;
        transform.localEulerAngles = dir.ToEuler();
    }

    

    
}
