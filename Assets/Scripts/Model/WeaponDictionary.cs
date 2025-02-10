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
        public AbilityCatalog catalog;
    }

    public List<WeaponTypeAbilityCatalogPair> keyValuePairs = new();
    public Dictionary<WeaponTypes,AbilityCatalog> GetWeaponDictionary()
    {
        Dictionary<WeaponTypes, AbilityCatalog> dictionary = new();
        foreach(var pair in keyValuePairs)
        {
            dictionary[pair.type] = pair.catalog;
        }
        return dictionary;
    }
}
