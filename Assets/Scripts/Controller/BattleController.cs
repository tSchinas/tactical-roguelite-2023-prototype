using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : StateMachine
{
    public CameraRig CameraRig;
    public Board Board;
    public LevelData LevelData;
    public Transform TileSelectionIndicator;
    public Point pos;
    public AbilityMenuPanelController abilityMenuPanelController;
    public Turn turn = new Turn();
    public List<Unit> units = new List<Unit>();
    public StatPanelController statPanelController;
    //heroPrefab,currentUnit,currentTile are placeholders
    //public GameObject heroPrefab;
    public Tile currentTile { get { return Board.GetTile(pos); } }
    //private BoardController boardScript;
    public static BattleController instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        //boardScript = GetComponent<BoardController>;
    }
    // Start is called before the first frame update
    void Start()
    {
        //boardScript.SetupScene();
        ChangeState<InitBattleState>();
    }

    
}
