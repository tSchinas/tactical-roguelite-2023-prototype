using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
//    #region Constants

//    const int turnActivation = 1000;
//    const int turnCost = 500;
//    const int moveCost = 300;
//    const int actionCost = 200;
//#endregion

    #region Notifications
    public const string RoundBeganNotification = "TurnManager.roundBegan";
    public const string TurnCheckNotification = "TurnManager.turnCheck";
    public const string TurnBeganNotification = "TurnManager.TurnBeganNotification";
    public const string TurnCompletedNotification = "TurnManager.turnCompleted";
    public const string RoundEndedNotification = "TurnManager.roundEnded";
    #endregion

    #region Public
    public IEnumerator Round()
    {
        BattleController bc = GetComponent<BattleController>(); ;
        while (true)
        {
            this.PostNotification(RoundBeganNotification);

            List<Unit> units = new List<Unit>(bc.units);


            units.Sort((a, b) =>
            {
                bool aIsHero = a is PlayableUnit;
                bool bIsHero = b is PlayableUnit;

                if (aIsHero && !bIsHero)
                    return -1;
                if (!aIsHero && bIsHero)
                    return 1;
                return 0;
            });
            for (int i = units.Count - 1; i >= 0; --i)
            {
                if (CanTakeTurn(units[i]))
                {
                    bc.turn.Change(units[i]);
                    units[i].PostNotification(TurnBeganNotification);

                    yield return units;
                }   
                
            }

            this.PostNotification(RoundEndedNotification);
        }
    }
    #endregion

    //#region Private
    bool CanTakeTurn(Unit target)
    {
        BaseException exc = new BaseException(CheckStatus(target));
        target.PostNotification(TurnCheckNotification, exc);
        return exc.toggle;
    }

    bool CheckStatus(Unit unit)
    {
        Status status = unit.GetComponentInChildren<Status>();
        if (status.GetComponentInChildren<KnockOutStatusEffect>())
        {
            return false;
        }
        return true;
    }
    //int GetCounter(Unit target)
    //{
    //    return target.GetComponent<Stats>()[StatTypes.CTR];
    //}
    //#endregion
}