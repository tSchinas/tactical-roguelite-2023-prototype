#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using System.IO;
using UnityEngine;



public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private GameObject tileViewPrefab;
    [SerializeField] private GameObject tileSelectionIndicatorPrefab;
    [SerializeField] private int width = 10; //specifies maximum width (x) of board
    [SerializeField] private int depth = 10; //specifies maximum depth (z) of board

    [SerializeField]
    private int
        height = 8; //specifies maximum height (step units as defined in tile script, smaller than world units) of board. step units affect a unit's ability to traverse terrain based on stats i.e Jump

    [SerializeField] private Point pos;
    [SerializeField] private LevelData levelData; //facilitates loading of previously saved boards to edit later
    private Transform _marker;

    private readonly Dictionary<Point, Tile>
        tiles = new(); //determine whether board contains tile based on specified coordinate position and grab reference

    private Transform marker //checks whether object is instantiated and instantiates if necessary
    {
        get
        {
            if (_marker == null)
            {
                var instance = Instantiate(tileSelectionIndicatorPrefab);
                _marker = instance.transform;
            }

            return _marker;
        }
    }

    public void GrowArea() //triggered via editor inspector script
    {
        var r = RandomRect();
        GrowRect(r);
    }

    public void ShrinkArea() //triggered via editor inspector script
    {
        var r = RandomRect();
        ShrinkRect(r);
    }

    private Rect RandomRect() //generates a Rect struct somewhere in the region specified by max extents
    {
        var x = Random.Range(0, width);
        var y = Random.Range(0, depth);
        var w = Random.Range(1, width - x + 1);
        var h = Random.Range(1, depth - y + 1);
        return new Rect(x, y, w, h);
    }

    private void GrowRect(Rect rect) //loop through range of positions in generated Rect, grow specified tile
    {
        for (var y = (int)rect.yMin; y < (int)rect.yMax; ++y)
        for (var x = (int)rect.xMin; x < (int)rect.xMax; ++x)
        {
            var p = new Point(x, y);
            GrowSingle(p);
        }
    }

    private void ShrinkRect(Rect rect) //loop through range of positions in generated Rect, shrink specified tile
    {
        for (var y = (int)rect.yMin; y < (int)rect.yMax; ++y)
        for (var x = (int)rect.xMin; x < (int)rect.xMax; ++x)
        {
            var p = new Point(x, y);
            ShrinkSingle(p);
        }
    }

    private Tile Create() //creates tile
    {
        var instance = Instantiate(tileViewPrefab);
        instance.transform.parent = transform;
        return instance.GetComponent<Tile>();
    }

    private Tile GetOrCreate(Point p)
    {
        if (tiles.ContainsKey(p)) //check if tile exists
            return tiles[p];

        var t = Create();
        t.Load(p, 0);
        tiles.Add(p, t);

        return t;
    }

    private void GrowSingle(Point p)
    {
        var t = GetOrCreate(p);
        if (t.height < height)
            t.Grow();
    }

    private void ShrinkSingle(Point p)
    {
        if (!tiles.ContainsKey(p)) //check if tile exists but don't create one if it doesn't
            return;

        var t = tiles[p];
        t.Shrink();

        if (t.height <= 0) //destroy tile if height is less than or equal to zero
        {
            tiles.Remove(p);
            DestroyImmediate(t.gameObject);
        }
    }

    public void Grow()
    {
        GrowSingle(pos);
    }

    public void Shrink()
    {
        ShrinkSingle(pos);
    }

    public void UpdateMarker()
    {
        var t = tiles.ContainsKey(pos) ? tiles[pos] : null;
        marker.localPosition = t != null ? t.Center : new Vector3(pos.x, 0, pos.y);
    }

    public void Clear() //clear the board and start over
    {
        for (var i = transform.childCount - 1; i >= 0; --i)
            DestroyImmediate(transform.GetChild(i).gameObject);
        tiles.Clear();
    }

    public void Save() //saves level
    {
#if UNITY_EDITOR
        var filePath = Application.dataPath + "/Resources/Levels";
        if (!Directory.Exists(filePath))
            CreateSaveDirectory();

        var board = ScriptableObject.CreateInstance<LevelData>();
        board.tiles = new List<Vector3>(tiles.Count);
        foreach (var t in tiles.Values)
            board.tiles.Add(new Vector3(t.pos.x, t.height, t.pos.y));

        var fileName = string.Format("Assets/Resources/Levels/{1}.asset", filePath, name);
        AssetDatabase.CreateAsset(board, fileName);
#endif
    }

    private void CreateSaveDirectory()
    {
        #if UNITY_EDITOR
        var filePath = Application.dataPath + "/Resources";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets", "Resources");
        filePath += "/Levels";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets/Resources", "Levels");
        AssetDatabase.Refresh();
#endif
    }

    public void Load() //loads saved level
    {

        Clear();
        if (levelData == null)
            return;

        foreach (var v in levelData.tiles)
        {
            var t = Create();
            t.Load(v);
            tiles.Add(t.pos, t);
        }
    }
}