using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
    public ItemType buyItemType;
    public Sprite itemSprite;
    public int itemPrice = 100;

    protected virtual void Start()
    {
        
        button.onClick.AddListener(delegate { OnBuy(default); });
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
        moneyText.text = itemPrice.ToString();
    }

    private void OnBuyItemSuccessful()
    {
        button.interactable = canBuyAgain;
    }

    #endregion

    #region Button Event

    protected virtual void OnBuy(WeaponDetails_SO data)
    {
        EventHandler.CallBuyItem(buyItemType, data, itemPrice);
        Debug.Log($"BUY {itemPrice}");
    }

    #endregion
    
}
