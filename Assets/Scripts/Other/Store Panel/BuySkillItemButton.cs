using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySkillItemButton : BuyButton
{
    protected override void Start()
    {
        // get data from GameManager
        buyItemDetails.itemName = GameManager.Instance.playerDetail.skillItemName;
        itemSprite = GameManager.Instance.playerDetail.skillItemSprite;

        // Update UI
        itemIconImage.sprite = itemSprite;
        itemNameText.text = buyItemDetails.itemName;

        // Add Listener
        button.onClick.AddListener(OnBuy);;
    }
}
