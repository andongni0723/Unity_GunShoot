using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StoreCheckWindow : MonoBehaviour
{
    [Header("Component")] 
    public Button correctButton;
    public Button cancelButton;
    public TextMeshProUGUI buyItemNameText;

    [Header("Setting")] 
    private WeaponDetails_SO buyItemDetail;
    private int buyItemPrice;

    private void Awake()
    {
        correctButton.onClick.AddListener(OnPressCorrect);
        cancelButton.onClick.AddListener(OnPressCancel);
    }

    #region Event

    private void OnEnable()
    {
        EventHandler.BuyItem += OnBuyItem; // Update UI
    }

    private void OnBuyItem(ItemType itemType, WeaponDetails_SO data, int itemPrice)
    {
        buyItemDetail = data;
        buyItemNameText.text = buyItemDetail.weaponName;
        buyItemPrice = itemPrice;
        Debug.Log($"check {itemPrice}");

    }

    #endregion

    #region Button Event

    public void OnPressCorrect()
    {
        //Debug.Log($"Press {buyItemPrice}");
        EventHandler.CallCorrectBuyItem(buyItemDetail, buyItemPrice);
    }

    public void OnPressCancel()
    {
        EventHandler.CallCancelBuyItem();
    }

    #endregion
}
