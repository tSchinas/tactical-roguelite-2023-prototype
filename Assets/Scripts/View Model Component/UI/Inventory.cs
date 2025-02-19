using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inventory;

    public List<PartyMemberInfo> partyMemberInfos;
    public BattleController bc;

    private void Start()
    {
        bc = FindFirstObjectByType<BattleController>();
    }

    public void InitializeUI()
    {
        Vector2 offset = new();
        offset.x = -450;
        for (int i = 0; i < partyMemberInfos.Count; ++i)
        {
            PlayableUnit pu = bc.playerUnits[i].GetComponent<PlayableUnit>();
            partyMemberInfos[i].InitializeInfo(pu, offset);
            offset.x += 450;
        }
    }
    
}
