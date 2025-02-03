using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class InitBattleState : BattleState
{
    
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        board.Load(LevelData);
        Point p = new Point((int)LevelData.tiles[0].x, (int)LevelData.tiles[0].z);
        SelectTile(p);
        
        SpawnTestUnits(); //new placeholder function
        InitializeUnitLists();
        yield return null;
        owner.facingIndicator.gameObject.SetActive(false);
        owner.ChangeState<CutsceneState>(); 
        Debug.Log("Changed to Cutscene State.");
        //owner.ChangeState<SelectUnitState>();
        //Debug.Log("Changed to Select Unit State.");
    }

    void SpawnTestUnits()
    {
        string[] recipes = new string[]
        {
            "Hero", "Light Bandit"
        };
        List<Tile> locations = new List<Tile>(board.tiles.Values);
        for (int i = 0; i < recipes.Length; ++i)
        {
            int level = UnityEngine.Random.Range(9, 12);
            GameObject instance = UnitFactory.Create(recipes[i], level);
            int random = UnityEngine.Random.Range(0, locations.Count);
            Tile randomTile = locations[random];
            locations.RemoveAt(random);
            Unit unit = instance.GetComponent<Unit>();
            unit.Place(randomTile);
            unit.dir = (Directions)UnityEngine.Random.Range(0, 4);
            unit.Match();
            units.Add(unit);
        }
        SelectTile(units[0].tile.pos);
    
    
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
