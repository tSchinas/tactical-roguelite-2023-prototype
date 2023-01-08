using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum EquipSlots
{
    None=0,
    MainWeapon=1<<0,
    SubWeapon=1<<1,
    Armor=1<<2,
    Artifact=1<<3
}
