using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System.Linq;

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
    public virtual void OnEquip(PlayableUnit owner, int slot)
    {

    }

    public T GetStat<T>(string propertyName)
    {
        FieldInfo field = GetType().GetField(propertyName, BindingFlags.Public | BindingFlags.Instance);
        if (field != null && field.FieldType == typeof(T))
        {
            return (T)field.GetValue(this);
        }

        Debug.LogWarning($"Property '{propertyName}' not found or type mismatch.");
        return default;
    }

    public virtual void SpawnItem()
    {
        
    }
}
