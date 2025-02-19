using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetValueFromDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private List<TMP_Dropdown.OptionData> options = new();
    public void GetDropdownValue()
    {
        int pickedEntryIndex = dropdown.value;
        string selectedOption = dropdown.options[pickedEntryIndex].text;
    }
    private void AddNewOption(string text)
    {
        dropdown.options.Add(new TMP_Dropdown.OptionData(text));
        dropdown.AddOptions(options);
        dropdown.RefreshShownValue();
    }
    private void RemoveOption(int index)
    {
        dropdown.options.RemoveAt(index);
        if(dropdown.value == index)
        {
            dropdown.value = 0;
        }
        dropdown.RefreshShownValue();
    }
    //private void SetActionOfDropdown()
    //{
    //    dropdown.onValueChanged.RemoveListener();
    //    dropdown.onValueChanged.AddListener();
    //}

}
