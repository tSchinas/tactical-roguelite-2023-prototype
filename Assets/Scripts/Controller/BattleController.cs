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
    public LevelData LevelData;
    public Transform TileSelectionIndicator;
    public Point pos;
    public AbilityMenuPanelController abilityMenuPanelController;
    public Turn turn = new Turn();
    public List<Unit> units = new List<Unit>();
    public List<Unit> playerUnits = new List<Unit>();
    public List<Unit> enemyUnits = new List<Unit>();

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
    //public void StartBattle()
    //{
    //    StartCoroutine(GameLoop());
    //}
    //private IEnumerator GameLoop()
    //{
    //    while (true)
    //    {
    //        yield return StartCoroutine(EnemyPlanningPhase());
    //        yield return StartCoroutine(PlayerTurnPhase());
    //        yield return StartCoroutine(EnemyExecutionPhase());
    //    }
    //}

    //private IEnumerator EnemyPlanningPhase()
    //{
    //    currentState = TurnState.EnemyPlanning;
    //    Debug.Log("Enemy is planning their moves.");

    //    foreach (var enemy in enemyUnits)
    //    {
    //        //enemy.PlanAction();
    //    }

    //    yield return new WaitForSeconds(1f);
    //}

    //private IEnumerator PlayerTurnPhase()
    //{
    //    currentState = TurnState.PlayerTurn;
    //    Debug.Log("Player's turn. Waiting for player input.");

    //    while (!PlayerHasEndedTurn())
    //    {
    //        yield return null;
    //    }
    //}

    //private IEnumerator EnemyExecutionPhase()
    //{
    //    currentState = TurnState.EnemyExecution;
    //    Debug.Log("Enemies executing attacks.");

    //    foreach (var enemy in enemyUnits)
    //    {
    //        //enemy.ExecuteAction();
    //        yield return new WaitForSeconds(0.5f);
    //    }

    //    yield return new WaitForSeconds(1f);
    //}

    //private bool PlayerHasEndedTurn()
    //{
    //    return Input.GetKeyDown(KeyCode.Space);
    //}

}
