using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : StateMachine
{
    public enum TurnState { EnemyPlanning, PlayerTurn, EnemyExecution }
    public TurnState currentState;

    //public BattleMessageController battleMessageController;
    public ComputerPlayer cpu;
    public FacingIndicator facingIndicator;
    public CameraRig CameraRig;
    public Board Board;
    public LevelDataCatalog levelCatalog;
    public LevelData LevelData;
    public EnemySetCatalog enemyCatalog;
    public UnitSet enemySet;
    public UnitSet heroSet;
    public Transform TileSelectionIndicator;
    public Point pos;
    public AbilityMenuPanelController abilityMenuPanelController;
    public Turn turn = new Turn();
    public List<Unit> units = new List<Unit>();
    public List<Unit> playerUnits = new List<Unit>();
    public List<Unit> enemyUnits = new List<Unit>();
    public Transform offWorldTransform;
    public AutoStatusController autoStatusController;


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
        PickLevel();
        PickEnemies();
        //boardScript = GetComponent<BoardController>;
    }
    // Start is called before the first frame update
    void Start()
    {
        //boardScript.SetupScene();
        ChangeState<InitBattleState>();
    }
    public IEnumerator round;
    void PickLevel()
    {
        if (levelCatalog != null)
        {
            LevelData = levelCatalog.GetRandomBoard();
        }
        else
        {
            Debug.LogError("LevelDataCatalog is not assigned!");
        }
    }
    void PickEnemies()
    {
        if (enemyCatalog != null)
        {
            enemySet = enemyCatalog.GetRandomEnemySet();
        }
        else
        {
            Debug.LogError("EnemySetCatalog is not assigned!");
        }
    }

}
