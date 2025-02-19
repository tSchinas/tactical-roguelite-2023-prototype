using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class ReinitializeBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Reinit());
    }

    IEnumerator Reinit()
    {
        foreach(Tile T in board.tiles.Values)
        {
            if(T.content!=null)
            {
                
                T.content = null;
            }
        }
        
        foreach (var item in owner.playerUnits)
        {
            item.transform.SetParent(owner.offWorldTransform);
            item.transform.position = new Vector3(0, 0, 0);
        }
        
        ResetVictoryCondition();
        ResetTurnManager();
        board.UnLoad();
        owner.levelCatalog.GetRandomBoard();
        board.Load(LevelData);
        Point p = new Point((int)LevelData.tiles[0].x, (int)LevelData.tiles[0].z);
        SelectTile(p);
        units.Clear();
        owner.enemyUnits.Clear();
        owner.enemyCatalog.GetRandomEnemySet();
        RespawnHeroes();
        SpawnEnemies(enemySet);

        InitializeUnitLists();
        yield return null;
        owner.ChangeState<CutsceneState>();
        ResetVictoryCondition();
    }

    private void ResetTurnManager()
    {
        TurnManager oldManager = owner.gameObject.GetComponent<TurnManager>();
        Destroy(oldManager);
        owner.round = owner.gameObject.AddComponent<TurnManager>().Round();
    }

    private void ResetVictoryCondition()
    {
        DefeatAllEnemiesVictoryCondition oldCondition = owner.gameObject.GetComponent<DefeatAllEnemiesVictoryCondition>();
        oldCondition.ResetVictor();
    }

    private void RespawnHeroes()
    {
        foreach (var unit in owner.playerUnits)
        {
            unit.transform.SetParent(owner.transform);
            unit.transform.position = new Vector3(0, 0, 0);
        }
        List<Tile> locations = new List<Tile>(board.tiles.Values);
        for (int l = 0; l < locations.Count; ++l)
        {
            if (locations[l].content)
            {
                locations.RemoveAt(l);
            }
        }
        for (int i = 0; i < owner.playerUnits.Count; ++i)
        {
            int random = UnityEngine.Random.Range(0, locations.Count);
            Tile randomTile = locations[random];
            locations.RemoveAt(random);
            PlayableUnit unit = owner.playerUnits[i].GetComponent<PlayableUnit>();
            unit.Place(randomTile);
            unit.dir = (Directions)UnityEngine.Random.Range(0, 4);
            unit.Match();
            units.Add(unit);
            
            Stats unitStats = unit.GetComponent<Stats>();
            if (unitStats[StatTypes.HP] < unitStats[StatTypes.MHP])
            {
                unitStats[StatTypes.HP] = unitStats[StatTypes.MHP];
            }
            if (unitStats[StatTypes.AP] < unitStats[StatTypes.MAP])
            {
                unitStats[StatTypes.AP] = unitStats[StatTypes.MAP];
            }
        }
    }
    void SpawnEnemies(UnitSet enemies)
    {

        List<Tile> locations = new List<Tile>(board.tiles.Values);
        for (int l = 0; l < locations.Count; ++l)
        {
            if (locations[l].content)
            {
                locations.RemoveAt(l);
            }
        }
        for (int i = 0; i < enemies.units.Length; ++i)
        {
            GameObject instance = UnitFactory.Create(enemies.units[i], 0);
            int random = UnityEngine.Random.Range(0, locations.Count);
            Tile randomTile = locations[random];
            locations.RemoveAt(random);
            Unit unit = instance.GetComponent<Unit>();
            unit.Place(randomTile);
            unit.dir = (Directions)UnityEngine.Random.Range(0, 4);
            unit.Match();
            units.Add(unit);
        }


    }
    private void InitializeUnitLists()
    {
        owner.playerUnits.Clear();
        owner.enemyUnits.Clear();
        foreach (var unit in units)
        {
            Alliances alliance;
            alliance = unit.GetComponentInParent<Alliance>().allianceType;
            if (alliance == Alliances.Hero)
            {
                owner.playerUnits.Add(unit);
            }
            else if (alliance == Alliances.Enemy)
            {
                owner.enemyUnits.Add(unit);
            }
        }
    }
}
