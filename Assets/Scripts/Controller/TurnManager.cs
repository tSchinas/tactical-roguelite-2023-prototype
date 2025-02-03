using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public List<Unit> playerUnits;
    public List<Unit> enemyUnits;


    public enum TurnState { EnemyPlanning, PlayerTurn, EnemyExecution }
    public TurnState currentState;

    private void Awake()
    {
        playerUnits = new List<Unit>();
        enemyUnits = new List<Unit>();
        InitializeUnitLists();
    }
    void Start()
    {
        InitializeUnitLists();
        StartCoroutine(GameLoop());
               
    }
    private void InitializeUnitLists()
    {
        List<Unit> units = GameObject.FindObjectsOfType<Unit>().ToList();

        foreach (var unit in units)
        {
            Alliances alliance;
            alliance = unit.GetComponentInParent<Alliance>().allianceType;
            if (alliance == Alliances.Hero)
            {
                playerUnits.Add(unit);
            }
            else if (alliance == Alliances.Enemy)
            {
                enemyUnits.Add(unit);
            }
        }
    }
    //continuously cycles through game phases
    private IEnumerator GameLoop()
    {
        while (true)
        {
            yield return StartCoroutine(EnemyPlanningPhase());
            yield return StartCoroutine(PlayerTurnPhase());
            yield return StartCoroutine(EnemyExecutionPhase());
        }
    }

    private IEnumerator EnemyPlanningPhase()
    {
        currentState = TurnState.EnemyPlanning;
        Debug.Log("Enemy is planning their moves.");

        // TODO: Move enemies and mark attack locations

        yield return new WaitForSeconds(1f); // Simulating enemy thinking time
    }

    private IEnumerator PlayerTurnPhase()
    {
        currentState = TurnState.PlayerTurn;
        Debug.Log("Player's turn. Waiting for player input.");

        // Wait until the player confirms their actions
        while (!PlayerHasEndedTurn())
        {
            yield return null;
        }
    }

    private IEnumerator EnemyExecutionPhase()
    {
        currentState = TurnState.EnemyExecution;
        Debug.Log("Enemies executing attacks.");

        // TODO: Process all enemy attacks based on their planned moves

        yield return new WaitForSeconds(1f); // Simulating attack animations
    }

    private bool PlayerHasEndedTurn()
    {
        // Placeholder condition - this should be tied to UI input
        return Input.GetKeyDown(KeyCode.Space);
    }
}