using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// decides which side of a battle you're on
/// bit mask for flexibility with status effects, etc.
/// </summary>
public enum Alliances
{
    None = 0,
    Neutral = 1 << 0,
    Hero = 1 << 1,
    Enemy = 1 << 2
}
