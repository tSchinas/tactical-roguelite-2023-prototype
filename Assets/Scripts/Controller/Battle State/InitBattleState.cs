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

        SpawnHeroes(heroSet);
        SpawnEnemies(enemySet);
        
        //SpawnTestWeapon(); //placeholder test function
        InitializeUnitLists();
        yield return null;
        owner.facingIndicator.gameObject.SetActive(false);
        owner.round = owner.gameObject.AddComponent<TurnManager>().Round();
        AddVictoryCondition();
        owner.ChangeState<CutsceneState>(); 
        Debug.Log("Changed to Cutscene State.");
        //owner.ChangeState<SelectUnitState>();
        //Debug.Log("Changed to Select Unit State.");
    }

    //void SpawnTestUnits()
    //{
    //    string[] recipes = new string[]
    //    {
    //        "Hero", "Light Bandit"
    //    };
    //    List<Tile> locations = new List<Tile>(board.tiles.Values);
    //    for (int i = 0; i < recipes.Length; ++i)
    //    {
    //        int level = UnityEngine.Random.Range(9, 12);
    //        GameObject instance = UnitFactory.Create(recipes[i], level);
    //        int random = UnityEngine.Random.Range(0, locations.Count);
    //        Tile randomTile = locations[random];
    //        locations.RemoveAt(random);
    //        Unit unit = instance.GetComponent<Unit>();
    //        unit.Place(randomTile);
    //        unit.dir = (Directions)UnityEngine.Random.Range(0, 4);
    //        unit.Match();
    //        units.Add(unit);
    //    }
    //    SelectTile(units[0].tile.pos);
    
    
    //}
    void SpawnHeroes(UnitSet heroes)
    {
        
        List<Tile> locations = new List<Tile>(board.tiles.Values);
        for (int l = 0; l<locations.Count; ++l)
        {
            if (locations[l].content)
            {
                locations.RemoveAt(l);
            }
        }
        for (int i = 0; i < heroes.units.Length; ++i)
        {
            GameObject instance = UnitFactory.Create(heroes.units[i], 0);
            int random = UnityEngine.Random.Range(0, locations.Count);
            Tile randomTile = locations[random];
            locations.RemoveAt(random);
            PlayableUnit unit = instance.GetComponent<PlayableUnit>();
            unit.Place(randomTile);
            unit.dir = (Directions)UnityEngine.Random.Range(0, 4);
            unit.Match();
            units.Add(unit);
            if (unit._eqMainWeapon == null)
            {
                WeaponTypes newType;
                float roll = UnityEngine.Random.value;
                if (roll >= .5f)
                {
                    newType = WeaponTypes.Dagger;
                }
                else
                    newType = WeaponTypes.Wand;

                //WeaponTypes newType = (WeaponTypes)UnityEngine.Random.Range(minVal, maxVal);
                GameObject newWeapon = WeaponFactory.Create(newType);
                unit._eqMainWeapon = newWeapon.GetComponent<Weapon>();
                newWeapon.transform.SetParent(unit.transform);
            }
            unit.EvaluateAbilityCatalog(unit);
        }
        
        SelectTile(units[0].tile.pos);
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


    //void SpawnTestWeapon()
    //{
    //    WeaponTypes[] weapons = new WeaponTypes[]
    //    {
    //        WeaponTypes.Dagger,
    //        WeaponTypes.Wand
    //    };
    //    PlayableUnit unit = FindObjectOfType<PlayableUnit>();
    //    for (int i = 0; i < weapons.Length; ++i)
    //    {

    //        GameObject instance = WeaponFactory.Create(weapons[i]);
    //        if (!unit._eqMainWeapon)
    //            unit._eqMainWeapon = instance.GetComponent<Weapon>();
    //        else
    //            unit._eqSubWeapon = instance.GetComponent<Weapon>();

    //        instance.transform.SetParent(unit.transform);
    //    }
    //    unit.EvaluateAbilityCatalog(unit);
    //}
    void AddVictoryCondition()
    {
        DefeatAllEnemiesVictoryCondition vc = owner.gameObject.AddComponent<DefeatAllEnemiesVictoryCondition>();
        
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
