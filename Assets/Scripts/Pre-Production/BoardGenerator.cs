using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public class BoardGenerator : MonoBehaviour
{
    [SerializeField] GameObject tileViewPrefab;
    [SerializeField] GameObject tileSelectionIndicatorPrefab;
    [SerializeField] int width = 10; //specifies maximum width (x) of board
    [SerializeField] int depth = 10; //specifies maximum depth (z) of board
    [SerializeField] int height = 8; //specifies maximum height (step units as defined in tile script, smaller than world units) of board. step units affect a unit's ability to traverse terrain based on stats i.e Jump
    [SerializeField] Point pos;
    [SerializeField] LevelData levelData; //facilitates loading of previously saved boards to edit later

    Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>(); //determine whether board contains tile based on specified coordinate position and grab reference
    Transform marker //checks whether object is instantiated and instantiates if necessary
    {
        get
        {
            if (_marker == null)
            {
                GameObject instance = Instantiate(tileSelectionIndicatorPrefab) as GameObject;
                _marker = instance.transform;
            }
            return _marker;
        }
    }
    Transform _marker;

    public void GrowArea() //triggered via editor inspector script
    {
        Rect r = RandomRect();
        GrowRect(r);
    }

    public void ShrinkArea() //triggered via editor inspector script
    {
        Rect r = RandomRect();
        ShrinkRect(r);
    }

    Rect RandomRect() //generates a Rect struct somewhere in the region specified by max extents
    {
        int x = UnityEngine.Random.Range(0, width);
        int y = UnityEngine.Random.Range(0, depth);
        int w = UnityEngine.Random.Range(1, width - x + 1);
        int h = UnityEngine.Random.Range(1, depth - y + 1);
        return new Rect(x, y, w, h);
    }

    void GrowRect(Rect rect) //loop through range of positions in generated Rect, grow specified tile
    {
        for (int y = (int)rect.yMin; y<(int)rect.yMax; ++y)
        {
            for (int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
            {
                Point p = new Point(x, y);
                GrowSingle(p);
            }
        }
    }
    void ShrinkRect(Rect rect) //loop through range of positions in generated Rect, shrink specified tile
    {
        for (int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
        {
            for (int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
            {
                Point p = new Point(x, y);
                ShrinkSingle(p);
            }
        }
    }

    Tile Create() //creates tile
    {
        GameObject instance = Instantiate(tileViewPrefab) as GameObject;
        instance.transform.parent = transform;
        return instance.GetComponent<Tile>();
    }

    Tile GetOrCreate (Point p)
    {
        if (tiles.ContainsKey(p)) //check if tile exists
            return tiles[p];

        Tile t = Create();
        t.Load(p, 0);
        tiles.Add(p, t);

        return t;
    }

    void GrowSingle (Point p)
    {
        Tile t = GetOrCreate(p);
        if (t.height < height)
            t.Grow();
    }

    void ShrinkSingle (Point p)
    {
        if (!tiles.ContainsKey(p)) //check if tile exists but don't create one if it doesn't
            return;

        Tile t = tiles[p];
        t.Shrink();

        if (t.height <=0) //destroy tile if height is less than or equal to zero
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
        Tile t = tiles.ContainsKey(pos) ? tiles[pos] : null;
        marker.localPosition = t != null ? t.Center : new Vector3(pos.x, 0, pos.y);
    }

    public void Clear() //clear the board and start over
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
            DestroyImmediate(transform.GetChild(i).gameObject);
        tiles.Clear();
    }

    public void Save() //saves level
    {
        string filePath = Application.dataPath + "/Resources/Levels";
        if (!Directory.Exists(filePath))
            CreateSaveDirectory();

        LevelData board = ScriptableObject.CreateInstance<LevelData>();
        board.tiles = new List<Vector3>(tiles.Count);
        foreach (Tile t in tiles.Values)
            board.tiles.Add(new Vector3(t.pos.x, t.height, t.pos.y));

        string fileName = string.Format("Assets/Resources/Levels/{1}.asset", filePath, name);
        AssetDatabase.CreateAsset(board, fileName);
    }

    void CreateSaveDirectory()
    {
        string filePath = Application.dataPath + "/Resources";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets", "Resources");
        filePath += "/Levels";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets/Resources", "Levels");
        AssetDatabase.Refresh();
    }

    public void Load() //loads saved level
    {
        Clear();
        if (levelData == null)
            return;

        foreach (Vector3 v in levelData.tiles)
        {
            Tile t = Create();
            t.Load(v);
            tiles.Add(t.pos, t);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
