using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : Item
{
    public WeaponTypes type;
    public override void OnEquip(PlayableUnit owner, int slot)
    {

        return;
    }

    

    public override void SpawnItem()
    {
        base.SpawnItem();
        GameObject newWeapon = WeaponFactory.Create(type);

    }
}
