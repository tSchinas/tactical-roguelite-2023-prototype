using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : Item
{
    public WeaponTypes type;
    public override void OnEquip(bool slot)
    {
        PlayableUnit owner = GetComponentInParent<PlayableUnit>();
        if (slot)
            owner.eqMainWeapon = this;
        else
            owner.eqSubWeapon = this;

        ParseEquippedWeapons(owner);
    }

    private void ParseEquippedWeapons(PlayableUnit obj)
    {
        AbilityCatalog oldCatalog = obj.GetComponentInChildren<AbilityCatalog>();
        
    }
}
