using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayableUnit : Unit
{
    public Weapon _eqMainWeapon;
    public Weapon _eqSubWeapon;
    public Armor _eqArmor;
    public Artifact _eqArtifact;

    public Weapon eqMainWeapon
    {
        get => _eqMainWeapon;
        set
        {
            if(_eqMainWeapon != value)
            {
                _eqMainWeapon = value;
                
            }
        }
    }

    public Weapon eqSubWeapon
    {
        get => _eqSubWeapon;
        set
        {
            if (_eqSubWeapon != value)
            {
                _eqSubWeapon = value;
                //EvaluateAbilityCatalog(this);
            }
        }
    }

    
    public IEnumerator WeaponSwap()
    {
        Weapon cachedMainWeapon = eqMainWeapon;
        eqMainWeapon = eqSubWeapon;
        eqSubWeapon = cachedMainWeapon;
        EvaluateAbilityCatalog(this);
        yield return new WaitForSeconds(2);
    }

    public void EvaluateAbilityCatalog(PlayableUnit owner)
    {
        if (owner == null)
            return;

        AbilityCatalog oldCatalog = owner.GetComponentInChildren<AbilityCatalog>();
        if (oldCatalog!=null)
            Destroy(oldCatalog.gameObject);


        string mainWeaponType = owner.eqMainWeapon != null ? owner.eqMainWeapon.type.ToString() : "";
        string subWeaponType = owner.eqSubWeapon != null ? owner.eqSubWeapon.type.ToString() : "";
        UnitFactory.SwitchAbilityCatalog(owner.gameObject, mainWeaponType, subWeaponType);
    }
}
