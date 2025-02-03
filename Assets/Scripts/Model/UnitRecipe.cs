using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UnitRecipe : ScriptableObject
{
    public string model;
    public string weapon;
    public string subweapon;
    public string attack;
    public string abilityCatalog;
    public string strategy;
    public Locomotions locomotions;
    public Alliances alliance;
    public int hp;
    public int mhp;
    public int ap;
    public int map;
    public int atk;
    public int def;
    public int mov;
    public int jmp;
}
