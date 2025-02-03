using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilityCatalogRecipe : ScriptableObject
{
    [System.Serializable]
    public class Category
    {
        public string name;
        public string[] abilities;
    }
    public Category[] categories;
}
