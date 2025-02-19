using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMemberInfo : MonoBehaviour
{
    public struct PMIUIDisplay
    {
        public List<string> statStrings;
        
        
    }

    public GameObject uiPrefab;
    public PartyMemberUI pmui;
    public PlayableUnit unit;
    public Stats unitStats;
    public PMIUIDisplay UIInfo;
    public Vector2 offset;
    public void InitializeInfo(PlayableUnit pu, Vector2 v2)
    {
        UIInfo.statStrings = new();
        
        unit = pu;
        unitStats = unit.GetComponent<Stats>();

        MakeStrings();

        GameObject newUI = Instantiate(uiPrefab);
        pmui = newUI.GetComponent<PartyMemberUI>();
        
        
        
        newUI.GetComponent<PartyMemberUI>().Display(UIInfo, this, v2);

        

        

    }

    private void MakeStrings()
    {
        int atk = unitStats[StatTypes.ATK];
        int def = unitStats[StatTypes.DEF];
        int mhp = unitStats[StatTypes.MHP];
        int map = unitStats[StatTypes.MAP];
        int mov = unitStats[StatTypes.MOV];

        if (unit != null)
        {
            if (unit.eqMainWeapon != null)
            {
                atk += unit.eqMainWeapon.atkBonus;
                def += unit.eqMainWeapon.defBonus;
                mhp += unit.eqMainWeapon.mhpBonus;
                map += unit.eqMainWeapon.mapBonus;
                mov += unit.eqMainWeapon.movBonus;
            }
            if (unit.eqSubWeapon != null)
            {
                atk += unit.eqSubWeapon.atkBonus;
                def += unit.eqSubWeapon.defBonus;
                mhp += unit.eqSubWeapon.mhpBonus;
                map += unit.eqSubWeapon.mapBonus;
                mov += unit.eqSubWeapon.movBonus;
            }
        }

        UIInfo.statStrings.Add("ATK: " + atk);
        UIInfo.statStrings.Add("DEF: " + def);
        UIInfo.statStrings.Add("HP: " + mhp);
        UIInfo.statStrings.Add("AP: " + map);
        UIInfo.statStrings.Add("MOV: " + mov);

    }

}
