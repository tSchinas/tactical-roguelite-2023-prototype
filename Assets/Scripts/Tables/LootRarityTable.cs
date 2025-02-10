using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LootRarityTable 
{
    public static float commonWeight = 0.6f;
    public static float rareWeight = 0.3f;
    public static float epicWeight = 0.07f;
    public static float legendaryWeight = 0.03f;

    private static Dictionary<ItemRarity, float> rarityWeights = new Dictionary<ItemRarity, float>
    {
        { ItemRarity.Common, commonWeight },
        { ItemRarity.Rare, rareWeight},
        { ItemRarity.Epic, epicWeight},
        { ItemRarity.Legendary, legendaryWeight}
    };

    public static ItemRarity GetWeightedRandomRarity()
    {
        float totalWeight = 0f;
        float roll = UnityEngine.Random.value;

        foreach (var rarity in rarityWeights)
        {
            totalWeight += rarity.Value;
            if (roll < totalWeight) 
                return rarity.Key;
        }

        return ItemRarity.Common;
    }
}
