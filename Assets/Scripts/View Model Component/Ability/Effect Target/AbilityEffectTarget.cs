using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//determines whether or not the effect applies to whatever may or may not be located at the specified board tile
//i.e Cure spell which normally restores hit points to units might have a secondary effect of damaging the undead
public abstract class AbilityEffectTarget : MonoBehaviour
{
    public abstract bool IsTarget(Tile tile);
}
