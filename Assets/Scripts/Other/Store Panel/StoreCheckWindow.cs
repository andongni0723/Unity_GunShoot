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
    //private WeaponDetails_SO buyItemDetail;
    private BuyItemDetails buyItemDetails;

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

    private void OnBuyItem(BuyItemDetails data)
    {
        buyItemDetails = data;
        buyItemNameText.text = buyItemDetails.itemName;
        Debug.Log($"check {data.itemPrice}");
    }

    #endregion

    #region Button Event

    private void OnPressCorrect()
    {
        //Debug.Log($"Press {buyItemPrice}");
        EventHandler.CallCorrectBuyItem(buyItemDetails);
    }

    private void OnPressCancel()
    {
        EventHandler.CallCancelBuyItem();
    }

    #endregion
}
