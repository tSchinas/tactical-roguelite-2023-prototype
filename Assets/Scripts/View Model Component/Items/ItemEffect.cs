using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect : MonoBehaviour
{
    public abstract ItemEffectRarity rarity { get; set; }
}
