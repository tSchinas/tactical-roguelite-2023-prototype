using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AttackOption
{
    class Mark
    {
        public Tile tile;
        public bool isMatch;

        public Mark(Tile tile, bool isMatch)
        {
            this.tile = tile;
            this.isMatch = isMatch;
        }
    }

    public Tile target;
    public Directions direction;
    public List<Tile> areaTargets = new List<Tile>();
    public bool isCasterMatch; //tracker for whether target location is a good option to move to
    public Tile bestMoveTile { get; private set; }
    //public int bestAngleBasedScore { get; private set; }
    List<Mark> marks = new List<Mark>();
    List<Tile> moveTargets = new List<Tile>();

    public void AddMoveTarget(Tile tile)
    {
        // Dont allow moving to a tile that would negatively affect the caster
        if (!isCasterMatch && areaTargets.Contains(tile))
            return;
        moveTargets.Add(tile);
    }
    public void AddMark(Tile tile, bool isMatch)
    {
        marks.Add(new Mark(tile, isMatch));
    }

    public int GetScore(Unit caster, Ability ability)
    {
        GetBestMoveTarget(caster, ability);
        if (bestMoveTile == null)
        {
            Debug.LogWarning("AttackOption.GetScore() returning 0!");
            return 1;
        }
        int score = 1;
        for (int i = 0; i < marks.Count; ++i)
        {
            if (marks[i].isMatch)
                score++;
            else
                score--;
        }
        if (isCasterMatch && areaTargets.Contains(bestMoveTile))
            score++;
        return score;
    }
    void GetBestMoveTarget(Unit caster, Ability ability)
    {
        if (moveTargets.Count == 0)
        {
            Debug.LogWarning("AttackOption.GetBestMoveTarget() returned 0 move targets");
            return;
        }


        bestMoveTile = moveTargets[UnityEngine.Random.Range(0, moveTargets.Count)];
    }

    void FilterBestMoves(List<Tile> list)
    {
        if (!isCasterMatch)
            return;
        bool canTargetSelf = false;
        for (int i = 0; i < list.Count; ++i)
        {
            if (areaTargets.Contains(list[i]))
            {
                canTargetSelf = true;
                break;
            }
        }
        if (canTargetSelf)
        {
            for (int i = list.Count - 1; i >= 0; --i)
            {
                if (!areaTargets.Contains(list[i]))
                    list.RemoveAt(i);
            }
        }
    }
}