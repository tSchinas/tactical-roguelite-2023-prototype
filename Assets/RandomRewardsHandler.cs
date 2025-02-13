using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomRewardsHandler : MonoBehaviour
{
    public struct Input
    {
        public int gridCellIdx;
        public Vector2 areaDimensions;
        public Vector3 areaCenter;
        public Vector2 uiElementDimensions;
        public Vector3 offset;
    }
    public struct RandomDropUIElements
    {
        public Sprite itemImage;
        public Sprite backgroundImage;
        public String itemName;
        public List<String> bonusTexts;
    }

    public GameObject uiPrefab;
    public RandomDropUI randomDropUI;
    public UIRandomDropData dropData;
    private int amount;
    public Input input;
    private RandomDropUIElements randomDropUIElements;
    // Start is called before the first frame update
    void Start()
    {
        CalculateAmount();
        GenerateLoot(amount);
    }

    private void CalculateAmount()
    {
        //function that will calculate the number of rewards the player should get
        amount = 1;   
    }

    private void GenerateLoot(int amount)
    {
        randomDropUIElements.bonusTexts = new();
        
        for (int i = 0; i < amount; ++i)
        {
            randomDropUIElements.bonusTexts.Clear();
            float roll = UnityEngine.Random.value;
            WeaponTypes randomType = (WeaponTypes)UnityEngine.Random.Range(1, 2);
            if(roll >= 0f)
            {
                GameObject newWeapon = WeaponFactory.Create(randomType);
                GameObject newUI = GameObject.Instantiate(uiPrefab);
                
                randomDropUI = newUI.GetComponent<RandomDropUI>();
                //newUI.transform.SetParent(randomDropUI.parentTransform.transform);

                Weapon weapon = newWeapon.GetComponent<Weapon>();
                switch (weapon.type)
                {
                    case WeaponTypes.Bow:
                        {
                            break;
                        }
                    case WeaponTypes.Sword:
                        {
                            break;
                        }
                    case WeaponTypes.Dagger:
                        {
                            randomDropUIElements.itemImage = dropData.daggerIcon;
                            randomDropUIElements.itemName = "Dagger";
                            break;
                        }
                    case WeaponTypes.Wand:
                        {
                            randomDropUIElements.itemImage = dropData.wandIcon;
                            randomDropUIElements.itemName = "Wand";
                            break;
                        }
                }
                switch (weapon.rarity)
                {
                    case ItemRarity.Common:
                        {
                            randomDropUIElements.backgroundImage = dropData.commonBackground;
                            break;
                        }
                    case ItemRarity.Rare:
                        {
                            randomDropUIElements.backgroundImage = dropData.rareBackground;
                            break;

                        }
                    case ItemRarity.Epic:
                        {
                            randomDropUIElements.backgroundImage = dropData.epicBackground;
                            break;

                        }
                    case ItemRarity.Legendary:
                        {
                            randomDropUIElements.backgroundImage = dropData.legendaryBackground;
                            break;

                        }
                }
                
                if (weapon.atkBonus > 0)
                {
                    randomDropUIElements.bonusTexts.Add($"ATK +{weapon.atkBonus}");
                }
                if (weapon.defBonus > 0)
                {
                    randomDropUIElements.bonusTexts.Add($"DEF +{weapon.defBonus}");
                }
                if (weapon.mhpBonus > 0)
                {
                    randomDropUIElements.bonusTexts.Add($"HP +{weapon.mhpBonus}");
                }
                if (weapon.mapBonus > 0)
                {
                    randomDropUIElements.bonusTexts.Add($"AP +{weapon.mapBonus}");
                }
                if (weapon.movBonus > 0)
                {
                    randomDropUIElements.bonusTexts.Add($"MOV: +{weapon.movBonus}");
                }
                                
                randomDropUI.Display(randomDropUIElements);
            }
            //TODO: Other Factories
        }
    }

    //private void PopulateUI(RandomDropUI dropUI, GameObject obj, GameObject uiPrefab)
    //{
    //    int textCounts;
    //    WeaponTypes droppedType = obj.GetComponent<Weapon>().type;
    //    switch(droppedType)
    //    {
    //        case WeaponTypes.Bow:
    //            break;
    //        case WeaponTypes.Sword:
    //            break;
    //        case WeaponTypes.Dagger:
    //            dropUI._weaponImage = uiPrefab.AddComponent.
    //    }    
    //}
    // Update is called once per frame
    void Update()
    {
        
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
