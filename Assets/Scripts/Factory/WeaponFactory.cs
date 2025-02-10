using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory
{
    
    public static GameObject Create(WeaponTypes type)
    {
        GameObject obj = InstantiatePrefab(("Weapons/Weapon"));
        obj.name = type.ToString();
        Weapon thisWeapon = obj.GetComponent<Weapon>();
        AddItemType(thisWeapon, type);
        AddRarity(thisWeapon);
        ItemRarity rarity = obj.GetComponent<Weapon>().rarity;
        
        AddAttackBonus(thisWeapon, rarity);
        AddDefenseBonus(thisWeapon, rarity);
        AddHPBonus(thisWeapon, rarity);
        AddAPBonus(thisWeapon, rarity);
        AddMovementBonus(thisWeapon, rarity);
        AddEffects(thisWeapon, rarity);

        return obj;
    }

    private static void AddItemType(Weapon thisWeapon, WeaponTypes type)
    {
        thisWeapon.type = type;
    }

    private static void AddEffects(Weapon thisWeapon, ItemRarity rarity)
    {
        return;
    }

    private static void AddMovementBonus(Weapon thisWeapon, ItemRarity rarity)
    {
        float roll = UnityEngine.Random.value;
        
        switch (rarity)
        {
            case ItemRarity.Legendary:
                if (roll <= .2f)
                {
                    Debug.Log($"Adding movement bonus with a roll of {roll} to legendary item!");
                    thisWeapon.movBonus += 1;
                }
                break;
        }
    }

    private static void AddAPBonus(Weapon thisWeapon, ItemRarity rarity)
    {
        float roll = UnityEngine.Random.value;
        switch (rarity)
        {
            case ItemRarity.Legendary:
                if (roll <= .5f)
                    Debug.Log($"Adding AP bonus with a roll of {roll} to legendary item!");
                thisWeapon.mapBonus += 1;
                break;
        }
    }
    private static void AddHPBonus(Weapon thisWeapon, ItemRarity rarity)
    {
        float roll = UnityEngine.Random.value;
        switch (rarity)
        {
            case ItemRarity.Epic:
                if (roll <= .15f)
                    Debug.Log($"Adding HP bonus (+1) with a roll of {roll} to epic item!");
                thisWeapon.mhpBonus += 1;
                break;
            case ItemRarity.Legendary:
                if (roll <= .3f)
                    Debug.Log($"Adding HP bonus (+1) with a roll of {roll} to legendary item!");
                thisWeapon.mhpBonus += 1;
                break;
        }
    }
    private static void AddDefenseBonus(Weapon thisWeapon, ItemRarity rarity)
    {
        float roll = UnityEngine.Random.value;
        switch (rarity)
        {
            case ItemRarity.Epic:
                if (roll <= .1f)
                    Debug.Log($"Adding DEF bonus (+1) with a roll of {roll} to epic item!");
                thisWeapon.defBonus += 1;
                break;
            case ItemRarity.Legendary:
                if (roll <= .2f)
                    Debug.Log($"Adding DEF bonus (+2) with a roll of {roll} to legendary item!");
                thisWeapon.defBonus += 2;
                break;
        }
    }

    private static void AddAttackBonus(Weapon thisWeapon, ItemRarity rarity)
    {
        float roll = UnityEngine.Random.value;
        switch (rarity)
        {
            case ItemRarity.Common:
                if (roll <= .05f)
                {
                    Debug.Log($"Adding ATK bonus (+1) with a roll of {roll} to common item!");
                    thisWeapon.atkBonus += 1;
                }
                break;
            case ItemRarity.Rare:
                if (roll <= .25f)
                {
                    Debug.Log($"Adding ATK bonus with a roll of {roll} to rare item!");
                    thisWeapon.atkBonus += 1;
                }
                break;
            case ItemRarity.Epic:
                if (roll <= .4f)
                {
                    Debug.Log($"Adding ATK bonus (+1) with a roll of {roll} to epic item!");
                    thisWeapon.atkBonus += 1;
                }
                break;
            case ItemRarity.Legendary:
                if (roll <= .05f)
                {
                    Debug.Log($"Adding ATK bonus (+2) with a roll of {roll} to legendary item!");
                    thisWeapon.atkBonus += 2;
                    break;
                }
                else if (roll <= .95f)
                {
                    Debug.Log($"Adding ATK bonus (+1) with a roll of {roll} to legendary item!");
                    thisWeapon.atkBonus += 1;
                    break;
                }
                break;
        }
    }

    private static void AddRarity(Weapon thisWeapon)
    {
        ItemRarity randomRarity = LootRarityTable.GetWeightedRandomRarity();
        thisWeapon.GetComponent<Weapon>().rarity = randomRarity;
        Debug.Log($"Adding rarity {randomRarity} to weapon!");
    }

    static GameObject InstantiatePrefab(string name)
    {
        GameObject prefab = Resources.Load<GameObject>(name);
        if (prefab == null)
        {
            Debug.LogError("No Prefab for name: " + name);
            return new GameObject(name);
        }
        GameObject instance = GameObject.Instantiate(prefab);
        instance.name = instance.name.Replace("(Clone)", "");
        return instance;
    }
}
