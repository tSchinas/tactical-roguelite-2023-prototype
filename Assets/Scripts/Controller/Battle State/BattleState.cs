using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Buffers;
public abstract class BattleState : State
{
    protected BattleController owner;
    //holding reference to items in scene which states need to perform logic.
    //wrapping fields in properties to avoid adding duplicate pointers.
    //if battlecontroller reference changes or updates, states all still point to correct entity
    public CameraRig CameraRig { get { return owner.CameraRig; } }
    public Board board { get { return owner.Board; } }
    public LevelData LevelData { get { return owner.LevelData; }  }
    public UnitSet enemySet { get { return owner.enemySet; } }
    public UnitSet heroSet { get { return owner.heroSet; } }
    public Transform tileSelectionIndicator { get { return owner.tileSelectionIndicator; } }
    public Point pos { get { return owner.pos; } set { owner.pos = value; } }
    public AbilityMenuPanelController abilityMenuPanelController { get { return owner.abilityMenuPanelController; } }
    public Turn turn { get { return owner.turn; } }
    public List<Unit> units { get { return owner.units; } }
    public StatPanelController statPanelController { get { return owner.statPanelController; } }
    //public TurnManager turnManager { get { return owner.turnManager; } }

    protected Driver driver;
    public override void Enter()
    {
        driver = (turn.actor != null) ? turn.actor.GetComponent<Driver>() : null;
        base.Enter();
    }
    protected override void AddListeners()
    {
        if (driver == null || driver.Current == Drivers.Human)
        {
            InputController.moveEvent += OnMove;
            InputController.fireEvent += OnFire;
        }
    }
    protected virtual void Awake()
    {
        owner = GetComponent<BattleController>();//gets owner reference used above
    }

    protected override void RemoveListeners()
    {
        InputController.moveEvent -= OnMove;
        InputController.fireEvent -= OnFire;
    }

    //virtual so concrete subclasses are not required to override unless modifying functionality
    protected virtual void OnMove(object sender, InfoEventArgs<Point> e)
    {

    }

    protected virtual void OnFire(object sender, InfoEventArgs<int> e)
    {

    }

    //sets selected tile of game board, assuming tile at location.
    //added in base class because several states will make use of selected tile.
    protected virtual void SelectTile(Point p)
    {
        if (pos == p || !board.tiles.ContainsKey(p))
            return;
        pos = p;
        tileSelectionIndicator.localPosition = board.tiles[p].Center;
    }
    protected virtual Unit GetUnit (Point p)
    {
        Tile t = board.GetTile(p);
        GameObject content = t != null ? t.content : null;
        return content != null ? content.GetComponent<Unit>() : null;
    }
    protected virtual void RefreshPrimaryStatPanel(Point p)
    {
        Unit target = GetUnit(p);
        if (target != null)
            statPanelController.ShowPrimary(target.gameObject);
        else
            statPanelController.HidePrimary();
    }
    protected virtual void RefreshSecondaryStatPanel(Point p)
    {
        Unit target = GetUnit(p);
        if (target != null)
            statPanelController.ShowSecondary(target.gameObject);
        else
            statPanelController.HideSecondary();
    }
    protected virtual bool DidPlayerWin()
    {
        return owner.GetComponent<BaseVictoryCondition>().Victor == Alliances.Hero;
    }
    protected virtual bool IsBattleOver()
    {
        return owner.GetComponent<BaseVictoryCondition>().Victor != Alliances.None;
    }
}
 
