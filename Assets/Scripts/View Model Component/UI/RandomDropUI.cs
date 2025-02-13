using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class RandomDropUI : MonoBehaviour
{

    public Transform textParentTransform;
    public GameObject _backgroundContainer;
    public GameObject _itemContainer;
    public String itemName;

    public GameObject bonusTextPrefab;

    public Vector2 offset;

    public void Display(RandomRewardsHandler.RandomDropUIElements data)
    {
        Image backgroundSprite = _backgroundContainer.GetComponent<Image>();
        backgroundSprite.sprite = data.backgroundImage;
        Image itemSprite = _itemContainer.GetComponent<Image>();
        itemSprite.sprite = data.itemImage;
        
        itemName = data.itemName;
        
        for (int i = 0; i < data.bonusTexts.Count; ++i)
        {
            GameObject bonusObj = Instantiate(bonusTextPrefab, textParentTransform);
            TextMeshProUGUI bonusText = bonusObj.GetComponent<TextMeshProUGUI>();
            

            Vector2 newPosition = bonusText.GetComponent<RectTransform>().anchoredPosition;
            newPosition -= offset;
          
            bonusText.rectTransform.anchoredPosition = newPosition;
            bonusText.text = data.bonusTexts[i];

            offset.y = offset.y + 50;
            

        }
    }
}
