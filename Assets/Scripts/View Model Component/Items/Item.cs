using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int atkBonus = 0;
    public int defBonus = 0;
    public int mhpBonus = 0;
    public int mapBonus = 0;
    public int movBonus = 0;
    public ItemEffect commonEffect;
    public ItemEffect rareEffect;
    public ItemEffect epicEffect;
    public ItemEffect legendaryEffect;
    public ItemRarity rarity;

    public virtual void OnEquip()
    {

    }
    public virtual void OnEquip(bool slot)
    {

    }
}
