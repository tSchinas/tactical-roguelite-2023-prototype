
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataCatalog", menuName = "LevelDataCatalog")]
public class LevelDataCatalog : ScriptableObject
{
    public LevelData[] levels;
    
    public LevelData GetRandomBoard()
    {
        if (levels == null || levels.Length == 0)
        {
            Debug.LogError($"No level data available in catalog!");
            return null;
        }
        return levels[Random.Range(0, levels.Length)];

    }
}
