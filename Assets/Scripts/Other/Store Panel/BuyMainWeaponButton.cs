using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMainWeaponButton : BuyButton
{
    private WeaponDetails_SO itemData;
    protected override void Start()
    {
        itemSprite = GameManager.Instance.playerDetail.mainWeaponDetails.weaponSprite;
        itemNameText.text = GameManager.Instance.playerDetail.mainWeaponDetails.weaponName;
        itemData = GameManager.Instance.playerDetail.mainWeaponDetails;
        
        button.onClick.AddListener(delegate { OnBuy(itemData); });;
    }
}
