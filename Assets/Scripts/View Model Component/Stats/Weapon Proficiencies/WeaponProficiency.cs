using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponProficiency : MonoBehaviour, IExperienceGainer
{
    [System.Serializable]
    public struct WeaponProficiencyEntry
    {
        public WeaponTypes weaponType;
        public int currentExperience;
    }
    public struct RequiredExperience
    {
        public int lvone;
        public int lvtwo;
        public int lvthree;
    }
    [System.Serializable]
    public struct WeaponProficiencyLevel
    {
        public WeaponTypes weaponType;
        public int currentLevel;
    }

    [SerializeField] private List<WeaponProficiencyEntry> weaponProficiencies = new();
    [SerializeField] private List<WeaponProficiencyLevel> weaponLevels = new();

    private Dictionary<WeaponTypes, int> proficiencyDictionary;
    public RequiredExperience requiredExperience;
    private Dictionary<WeaponTypes, int> currentLevels;
    public string MainGainDisplay
    {
        get
        {
            string typetext = ownerUnit.eqMainWeapon.type.ToString();
            if (ownerUnit == null || ownerUnit.eqMainWeapon == null)
                return "+2 Main";
            return $"+2 "+typetext;
        }
    }
    public string SubGainDisplay
    {
        get
        {
            string typetext = ownerUnit.eqSubWeapon.type.ToString();
            if (ownerUnit == null || ownerUnit.eqSubWeapon == null)
                return "+1 Main";
            return $"+1 "+typetext;
        }
    }

    public string MainLvUpDisplay
    {
        get
        {
            if (ownerUnit == null || ownerUnit.eqMainWeapon == null)
            {
                return "Main Lv+";
            }
            return $"{ownerUnit._eqMainWeapon.type} Lv+";
        }
    }

    public string SubLvUpDisplay
    {
        get
        {
            if (ownerUnit == null || ownerUnit.eqSubWeapon == null)
            {
                return "Sub Lv+";
            }
            return $"{ownerUnit._eqSubWeapon.type} Lv+";
        }
    }
    
    private static PlayableUnit ownerUnit;

    private int expCurve = 20;
    private int mainExp = 2;
    private int subExp = 1;

    private void Start()
    {
        ownerUnit = GetComponent<PlayableUnit>();
        
    }
      

    private void Awake()
    {
        
        proficiencyDictionary = new Dictionary<WeaponTypes, int>();
        foreach (WeaponProficiencyEntry entry in weaponProficiencies)
        {
            proficiencyDictionary[entry.weaponType] = entry.currentExperience;
        }

        currentLevels = new Dictionary<WeaponTypes, int>();
        foreach (WeaponProficiencyLevel level in weaponLevels)
        {
            currentLevels[level.weaponType] = level.currentLevel;
        }

        requiredExperience.lvone = expCurve * 1;
        requiredExperience.lvtwo = requiredExperience.lvone + expCurve;
        requiredExperience.lvthree = requiredExperience.lvone + expCurve;
    }

    public void GainExperience(Weapon mainWeapon, Weapon subWeapon)
    {
        if (mainWeapon != null)
        {
            IncreaseExperience(mainWeapon.type, mainExp);
        }
        if (subWeapon != null)
        {
            IncreaseExperience(subWeapon.type, subExp);
        }
    }

    public int? GetCurrentExperience(WeaponTypes type)
    {
        if (proficiencyDictionary.TryGetValue(type, out int curExp))
        {
            return curExp;
        }
        Debug.LogError($"Weapon type {type} not found.");
        
        return null;
    }

    public int? GetCurrentLevel(WeaponTypes type)
    {
        if (currentLevels.TryGetValue(type, out int level))
        {
            return level;
        }
        Debug.LogError($"Weapon type {type} not found in level tracking.");
        return null;
    }

    

    //public void OnAbilityPerformed(object sender, object args)
    //{
    //    Ability ability = sender as Ability;
    //    if (ability == null)
    //        return;

    //    //PlayableUnit unit = ability.GetComponentInParent<PlayableUnit>();
    //    if (ability.GetComponentInParent<PlayableUnit>() == ownerUnit)
    //    {
    //        UpdateExperience(ownerUnit.eqMainWeapon, ownerUnit.eqSubWeapon);
    //    }
        
    //}

    //private void UpdateExperience(Weapon mainWeapon, Weapon subWeapon)
    //{
    //    if (mainWeapon != null)
    //    {
    //        IncreaseExperience(mainWeapon.type, mainExp);
    //    }

    //    if (subWeapon != null)
    //    {
    //        IncreaseExperience(subWeapon.type, subExp);
    //    }
    //}

    private void IncreaseExperience(WeaponTypes type, int amount)
    {
        if (!proficiencyDictionary.ContainsKey(type)) return;

        proficiencyDictionary[type] += amount;
        for (int i = 0; i < weaponProficiencies.Count; i++)
        {
            if (weaponProficiencies[i].weaponType == type)
            {
                weaponProficiencies[i] = new WeaponProficiencyEntry
                {
                    weaponType =  type,
                    currentExperience = proficiencyDictionary[type]
                };
                break;
            }
            Debug.Log($">>>{type} exp increased by {amount}!<<<");
        }

        int newLevel = CalculateLevel(proficiencyDictionary[type]);
        if(!currentLevels.ContainsKey(type) || currentLevels[type] < newLevel)
        {
            currentLevels[type] = newLevel;

            for(int i = 0; i<weaponLevels.Count;++i)
            {
                if (weaponLevels[i].weaponType == type)
                {
                    weaponLevels[i] = new WeaponProficiencyLevel
                    {
                        weaponType = type,
                        currentLevel = newLevel
                    };
                    break;
                }
            }
            Debug.Log($"Unit's {type} leveled up to {newLevel}!");
        }

        
    }

    private int CalculateLevel(int experience)
    {
        if (experience >= requiredExperience.lvthree) 
            return 3;
        else if (experience >= requiredExperience.lvtwo) 
            return 2;
        else
            return 1;
    }
}
