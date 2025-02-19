using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClaimRewardButton : MonoBehaviour
{
    public GameObject weapon;
    public BattleController bc;
    private readonly string claimedText = "Claimed!";

    private void Start()
    {
        bc = FindObjectOfType<BattleController>();
    }
    public IEnumerator ClaimReward()
    {
        bc.inventory.inventory.Add(weapon);
        //TextMeshPro text = this.gameObject.GetComponentInChildren<Button>().gameObject.GetComponentInChildren<TextMeshPro>();
        //text.text = claimedText;
        GameObject button = gameObject.GetComponentInChildren<Button>().gameObject;
        button.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        bc.ChangeState<PreBattlePreparationState>();
    }
    public void InitializeActor(GameObject weapon)
    {
        this.weapon = weapon;
    }
    public void RoutineWrapper()
    {
        Debug.LogWarning("Starting coroutine ClaimReward()!");
        StartCoroutine(ClaimReward());
    }
}
