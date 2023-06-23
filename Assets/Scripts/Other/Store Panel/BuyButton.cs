using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [Header("Component")] 
    public Button button;
    public Image itemIconImage;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI moneyText;
    
    [Header("Setting")]
    public bool canBuyAgain = true;
    [FormerlySerializedAs("buyItemDetail")] public BuyItemDetails buyItemDetails;
    public Sprite itemSprite;

    protected virtual void Start()
    {
        button.onClick.AddListener(OnBuy);
    }

    #region Event

    private void OnEnable()
    {
        EventHandler.StorePanelLoadingDone += OnStorePanelLoadingDone; // Update UI
        EventHandler.BuyItemSuccessful += OnBuyItemSuccessful; // Can buy Again UI
    }

    private void OnStorePanelLoadingDone()
    {
        itemIconImage.sprite = itemSprite;
        moneyText.text = buyItemDetails.itemPrice.ToString();
    }

    private void OnBuyItemSuccessful(BuyItemDetails data)
    {
        button.interactable = canBuyAgain;
    }

    #endregion

    #region Button Event

    protected virtual void OnBuy()
    {
        EventHandler.CallBuyItem(buyItemDetails);
        Debug.Log($"BUY {buyItemDetails.itemPrice}");
    }

    #endregion
    
}
