using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySetCatalog", menuName = "EnemySetCatalog")]
public class EnemySetCatalog : ScriptableObject
{
    public UnitSet[] enemySets;

    public UnitSet GetRandomEnemySet()
    {
        if (enemySets == null || enemySets.Length == 0)
        {
            Debug.LogError("No enemy data available in catalog!");
            return null;
        }
        return enemySets[Random.Range(0, enemySets.Length)];
    }
}
