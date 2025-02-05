using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UnitFactory 
{
    public static GameObject Create(string name, int level)
    {
        UnitRecipe recipe = Resources.Load<UnitRecipe>("Unit Recipes/" + name);
        if (recipe == null)
        {
            Debug.LogError("No unit recipe for name: " + name);
            return null;
        }
        return Create(recipe, level);
    }
    public static GameObject Create(UnitRecipe recipe, int level)
    {
        GameObject obj = InstantiatePrefab("Units/" + recipe.model);
        obj.name = recipe.name;
        obj.AddComponent<Unit>();
        AddStats(obj, recipe);
        AddLocomotion(obj, recipe.locomotions);
        //obj.AddComponent<Status>();
        //obj.AddComponent<Equipment>();
        //AddJob(obj, recipe.job);
        //AddRank(obj, recipe.level);
        //obj.AddComponent<Health>;
        //obj.Addcomponent<Mana>;
        AddAttack(obj, recipe.attack);
        AddAbilityCatalog(obj, recipe.abilityCatalog);
        AddAlliance(obj, recipe.alliance);
        AddAttackPattern(obj, recipe.strategy);
        return obj;
    }

    static GameObject InstantiatePrefab (string name)
    {
        GameObject prefab = Resources.Load < GameObject > (name);
        if (prefab == null)
        {
            Debug.LogError("No Prefab for name: " + name);
            return new GameObject(name);
        }
        GameObject instance = GameObject.Instantiate(prefab);
        instance.name = instance.name.Replace("(Clone)", "");
        return instance;
    }
    static void AddStats(GameObject obj, UnitRecipe recipe)
    {
        Stats s = obj.AddComponent<Stats>();
        
        s.SetValue(StatTypes.HP, recipe.hp, false);
        s.SetValue(StatTypes.MHP, recipe.mhp, false);
        s.SetValue(StatTypes.AP, recipe.ap, false);
        s.SetValue(StatTypes.MAP, recipe.map, false);
        s.SetValue(StatTypes.ATK, recipe.atk, false);
        s.SetValue(StatTypes.DEF, recipe.def, false);
        s.SetValue(StatTypes.MOV, recipe.mov, false);
        s.SetValue(StatTypes.JMP, recipe.jmp, false);
       
    }
    //static void AddJob (GameObject obj, string name)
    //{
    //    GameObject instance = InstantiatePrefab("Jobs/" + name);
    //    instance.transform.SetParent(obj.transform);
    //    Job job = instance.GetComponent<Job>();
    //    job.Employ();
    //    job.LoadDefaultStats();
    //}
    static void AddLocomotion(GameObject obj, Locomotions type)
    {
        switch (type)
        {
            case Locomotions.Walk:
                obj.AddComponent<WalkMovement>();
                break;
            case Locomotions.Fly:
                obj.AddComponent<FlyMovement>();
                break;
            case Locomotions.Teleport:
                obj.AddComponent<TeleportMovement>();
                break;
        }
    }
    static void AddAlliance (GameObject obj, Alliances type)
    {
        Alliance alliance = obj.AddComponent<Alliance>();
        alliance.allianceType = type;
    }
    static void AddAttack (GameObject obj, string name)
    {
        GameObject instance = InstantiatePrefab("Abilities/" + name);
        if (instance == null)
        {
            Debug.LogError("No Prefab for name: " + name);
            
        }
        instance.transform.SetParent(obj.transform);
    }
    static void AddAbilityCatalog (GameObject obj, string name)
    {
        GameObject main = new GameObject("Ability Catalog");
        main.transform.SetParent(obj.transform);
        main.AddComponent<AbilityCatalog>();

        AbilityCatalogRecipe recipe = Resources.Load<AbilityCatalogRecipe>("Ability Catalog Recipes/" + name);
        if (recipe == null)
        {
            Debug.LogError("No ability catalog recipe found with name: " + name);
            return;
        }
        for (int i=0; i<recipe.categories.Length;++i)
        {
            GameObject category = new GameObject(recipe.categories[i].name);
            category.transform.SetParent(main.transform);

            for (int j = 0; j < recipe.categories[i].abilities.Length; ++j)
            {
                string abilityName = string.Format("Abilities/{0}/{1}", recipe.categories[i].name, recipe.categories[i].abilities[j]);
                GameObject ability = InstantiatePrefab(abilityName);
                ability.name = recipe.categories[i].abilities[j];
                ability.transform.SetParent(category.transform);
            }
        }
    }

    static void AddAttackPattern(GameObject obj, string name)
    {
        Driver driver = obj.AddComponent<Driver>();
        if (string.IsNullOrEmpty(name))
        {
            driver.normal = Drivers.Human;
        }
        else
        {
            driver.normal = Drivers.Computer;
            GameObject instance = InstantiatePrefab("Attack Pattern/" + name);
            instance.transform.SetParent(obj.transform);
        }
    }
}
