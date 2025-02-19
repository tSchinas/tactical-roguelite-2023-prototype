using TMPro;
using UnityEngine;

public class PartyMemberUI : MonoBehaviour
{
    public RectTransform textParentTransform;
    public GameObject textPrefab;
    public TextMeshProUGUI charNameText;
    public Vector2 offset;
    public PartyMemberInfo pmi;
    public EquipNewWeapon equipMain;
    public EquipNewWeapon equipSub;

    private void Start()
    {
        charNameText.text = pmi.unit.gameObject.name;
        
    }

    public void Display(PartyMemberInfo.PMIUIDisplay data, PartyMemberInfo newPMI, Vector2 v2)
    {
        pmi = newPMI;
        offset.y = 50;
        charNameText.gameObject.GetComponent<RectTransform>().anchoredPosition += v2;
        for (int i = 0; i < data.statStrings.Count; ++i)
        {
            GameObject statObj = Instantiate(textPrefab, textParentTransform);
            TextMeshProUGUI newText = statObj.GetComponent<TextMeshProUGUI>();

            Vector2 newPosition = newText.GetComponent<RectTransform>().anchoredPosition;
            newPosition -= offset;
            
            newText.rectTransform.anchoredPosition = newPosition;
            newText.text = data.statStrings[i];

            offset.y += 50;

        }

        equipMain.selectedUnit = pmi.unit;
        equipSub.selectedUnit = pmi.unit;
        
        equipMain.inventory = FindObjectOfType<BattleController>().inventory;
        equipSub.inventory = FindObjectOfType<BattleController>().inventory;

    }
}