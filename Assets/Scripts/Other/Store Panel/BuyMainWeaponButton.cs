using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMainWeaponButton : BuyButton
{
    protected override void Start()
    {
        // get data from GameManager
        buyItemDetails.itemName = GameManager.Instance.playerDetail.mainWeaponDetails.weaponName;
        buyItemDetails.buyWeaponDetails = GameManager.Instance.playerDetail.mainWeaponDetails;
        itemSprite = GameManager.Instance.playerDetail.mainWeaponDetails.weaponSprite;

        // Update UI
        itemIconImage.sprite = itemSprite;
        itemNameText.text = buyItemDetails.itemName;

        // Add Listener
        button.onClick.AddListener(OnBuy);;
    }
}
