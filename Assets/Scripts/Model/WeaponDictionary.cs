using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponDictionary", menuName = "WeaponDictionary")]
public class WeaponDictionary : ScriptableObject
{
    
    [Serializable]
    public class WeaponTypeAbilityCatalogPair
    {
        public WeaponTypes type;
        public AbilityCatalogRecipe catalog;
    }

    public List<WeaponTypeAbilityCatalogPair> keyValuePairs = new();
    public Dictionary<WeaponTypes,AbilityCatalogRecipe> GetWeaponDictionary()
    {
        Dictionary<WeaponTypes, AbilityCatalogRecipe> dictionary = new();
        foreach(var pair in keyValuePairs)
        {
            dictionary[pair.type] = pair.catalog;
        }
        return dictionary;
    }
}
