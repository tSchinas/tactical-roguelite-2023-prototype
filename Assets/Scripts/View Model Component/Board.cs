using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    public Dictionary<Point, Tile> tiles = new();
    Color selectedTileColor = new Color(0, 1, 1, 1);
    Color defaultTileColor = new Color(1, 1, 1, 1);
    public Point min { get { return _min; } }
    public Point max { get { return _max; } }
    Point _min;
    Point _max;
    //public int columns = 10;
    //public int rows = 10;
    //public Counter gapCounter = new Counter(4, 9);
    //public GameObject[] floorTiles;
    

    //void InitializeList()//clears list gridPositions and prepares it to generate new board
    //{
    //    gridPositions.Clear();

    //    for (int x = 1; x < columns - 1; x++)
    //    {
    //        for (int z = 1; z < rows - 1; z++)
    //        {
    //            gridPositions.Add(new Vector3(x, 0f, z));
    //        }
    //    }
    //    for (int x = 1; x < columns - 1; x++)
    //    {
    //        for (int z = 1; z < rows - 1; z++)
    //        {
    //            enemyPositions.Add(new Vector3(x, 0f, z));
    //        }
    //    }
    //}

    //public void SetupScene(int level)
    //{
        //BoardSetup();
        //InitializeList();
        //LayoutAlliesAtRandom(allyUnits, allyCount, allyCount);
        //LayoutEnemyAtRandom(enemyUnits, enemyCount, enemyCount);
    //}
    //void BoardSetup()
    //{
    //    boardHolder = new GameObject("Board").transform;
    //    floorHolder = new GameObject("Floor").transform;

    //    floorHolder.transform.SetParent(boardHolder);

    //    for (int x = -1; x < columns + 1; x++)
    //    {
    //        for (int z = -1; z < columns + 1; z++)
    //        {
    //            GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
    //            GameObject instance = Instantiate(toInstantiate, new Vector3(x, 0f, z), Quaternion.identity) as GameObject;
    //            instance.transform.SetParent(floorHolder);
    //        }
    //    }
    //}

    

    //void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    //{
    //    int objectCount = Random.Range(minimum, maximum + 1);
    //    for (int i = 0; i < objectCount; ++i)
    //    {
    //        Vector3 randomPosition = RandomPosition();
    //        GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
    //        GameObject instance = Instantiate(tileChoice, randomPosition, Quaternion.identity) as GameObject;

    //    }
    //}

    



    public void Load(LevelData data)
    {
        _min = new Point(int.MaxValue, int.MaxValue);
        _max = new Point(int.MinValue, int.MinValue);

        for (int i = 0; i < data.tiles.Count; ++i)
        {
            GameObject instance = Instantiate(tilePrefab) as GameObject;
            instance.transform.SetParent(transform);
            Tile t = instance.GetComponent<Tile>();
            t.Load(data.tiles[i]);
            tiles.Add(t.pos, t);

            _min.x = Mathf.Min(_min.x, t.pos.x);
            _min.y = Mathf.Min(_min.y, t.pos.y);
            _max.x = Mathf.Max(_max.x, t.pos.x);
            _max.y = Mathf.Max(_max.y, t.pos.y);
        }
        
    }
    Point[] dirs = new Point[4]
    {
        new Point(0,1),
        new Point(0,-1),
        new Point(1,0),
        new Point(-1,0)
    };

    public List<Tile> Search(Tile start, Func<Tile, Tile, bool> addTile) //returns list of tiles starting from a specific tile that meet certain criteria
    {
        List<Tile> retValue = new();
        retValue.Add(start);

        ClearSearch();
        Queue<Tile> checkNext = new();
        Queue<Tile> checkNow = new();
        start.distance = 0;
        checkNow.Enqueue(start);

        while (checkNow.Count > 0)
        {
            Tile t = checkNow.Dequeue();
            for (int i = 0; i < 4; ++i)
            {
                Tile next = GetTile(t.pos + dirs[i]);
                if (next == null || next.distance <= t.distance + 1)
                    continue;
                if (addTile(t, next))
                {
                    next.distance = t.distance + 1;
                    next.prev = t;
                    checkNext.Enqueue(next);
                    retValue.Add(next);
                }
            }
            if (checkNow.Count == 0)
            {
                SwapReference(ref checkNow, ref checkNext);
            }
        }


        return retValue;
    }
    public Tile GetTile(Point p)
    {
        return tiles.ContainsKey(p) ? tiles[p] : null;
    }
    void ClearSearch()
    {
        foreach (Tile t in tiles.Values)
        {
            t.prev = null;
            t.distance = int.MaxValue;
        }
    }

    void SwapReference(ref Queue<Tile> a, ref Queue<Tile> b)
    {
        Queue<Tile> temp = a;
        a = b;
        b = temp;
    }

    public void SelectTiles(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
            tiles[i].GetComponent<Renderer>().material.SetColor("_Color", selectedTileColor);
    }
    public void DeSelectTiles(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
            tiles[i].GetComponent<Renderer>().material.SetColor("_Color", defaultTileColor);
    }
}
