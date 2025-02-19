using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipNewWeapon : MonoBehaviour
{
    public int switchInt;
    public PlayableUnit selectedUnit;
    public Inventory inventory;
    public void EquipNewDestroyOld(PlayableUnit pu, Inventory inv, int i)
    {
        switch(i)
        {
            case 0:
                GameObject oldMainWeapon = pu.eqMainWeapon.gameObject;

                inventory.inventory[0].transform.SetParent(pu.transform);
                pu.eqMainWeapon = inventory.inventory[0].GetComponent<Weapon>();
                
                pu.EvaluateAbilityCatalog(pu);
                
                if(oldMainWeapon)
                {
                    Destroy(oldMainWeapon);
                }
                inventory.inventory.RemoveAt(0);
                break;
            case 1:
                GameObject oldSubWeapon;
                if (pu.eqSubWeapon)
                {
                    oldSubWeapon = pu.eqSubWeapon.gameObject;
                }
                else
                    oldSubWeapon = null;

                inventory.inventory[0].transform.SetParent(pu.transform);
                pu.eqSubWeapon = inventory.inventory[0].GetComponent<Weapon>();

                pu.EvaluateAbilityCatalog(pu);

                if (oldSubWeapon != null)
                {
                    Destroy(oldSubWeapon);
                }
                inventory.inventory.RemoveAt(0);
                break;
        }
        BattleController bc = FindFirstObjectByType<BattleController>();
        bc.ChangeState<ReinitializeBattleState>();

    }
    public void OnClick()
    {
        int i = switchInt;
        PlayableUnit pu = selectedUnit;
        Inventory inv = inventory;
        EquipNewDestroyOld(pu, inv, i);
    }
}
