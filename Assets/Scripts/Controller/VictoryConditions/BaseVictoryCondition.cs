using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseVictoryCondition : MonoBehaviour
{
    public Alliances Victor{ get { return victor; } protected set { victor = value; } }
    Alliances victor = Alliances.None;
    protected BattleController bc;
    protected virtual void Awake()
    {
        bc = GetComponent<BattleController>();
    }
    protected virtual void OnEnable()
    {
        this.AddObserver(OnHPDidChangeNotification, Stats.DidChangeNotification(StatTypes.HP));
    }
    protected virtual void OnDisable()
    {
        this.RemoveObserver(OnHPDidChangeNotification, Stats.DidChangeNotification(StatTypes.HP));
    }
    protected virtual void OnHPDidChangeNotification(object sender, object args)
    {
        //CheckForGameOver();
    }
    //protected virtual bool IsDefeated(Unit unit)
    //{
    //    Stats health = unit.GetComponent<Stats>();
    //    if (health)
    //        return health.MinHP == health.HP;

    //    Stats stats = unit.GetComponent<Stats>();
    //    return stats[StatTypes.HP] == 0;
    //}
}
