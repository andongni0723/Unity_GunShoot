using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StorePanel : MonoBehaviour
{
    [Header("Component")] 
    public GameObject storePanel;
    public GameObject storePad;
    public CanvasGroup loadingBarParent;
    public CanvasGroup mainStoreWindow;
    public CanvasGroup checkPanelWindow;
    public CanvasGroup buyCorrectWindow;
    public CanvasGroup buyFailedWindow;
    public Slider loadingBar;
    public TextMeshProUGUI buyFailedMessageText;
    public TextMeshProUGUI currentMoneyText;

    [Header("Setting")] 
    public float panelFadeTime = 0.5f;
    public float loadingBarValueFadeTime = 0.8f;

    private bool isInit = false;

    private void Update()
    {
        if (storePanel.activeSelf && !isInit)
        {
            Init();
            isInit = true;
        }

        if (!storePanel.activeSelf)
        {
            isInit = false;
        }
    }

    private void Init()
    {
        UpdateUI();
        
        loadingBar.value = 0;
        loadingBarParent.alpha = 1;
        loadingBarParent.gameObject.SetActive(true);

        mainStoreWindow.alpha = 0;
        mainStoreWindow.gameObject.SetActive(false);
        
        checkPanelWindow.alpha = 0;
        checkPanelWindow.gameObject.SetActive(false);
            
        buyCorrectWindow.alpha = 0;
        buyCorrectWindow.gameObject.SetActive(false);
        
        buyFailedWindow.alpha = 0;
        buyFailedWindow.gameObject.SetActive(false);

        StartLoadingBarAnimation();
    }

    #region Event

    private void OnEnable()
    {
        EventHandler.CloseUI += OnCloseUI;         // Close Window
        EventHandler.OpenStorePanel += OnOpenStorePanel; // Open Panel
        EventHandler.BuyItem += OnBuyItem;               // switch window to check window
        EventHandler.CorrectBuyItem += OnCorrectBuyItem; // Check have money to buy
        EventHandler.CancelBuyItem += OnCancelBuyItem;   // Back to Main Store Window
    }

    private void OnDisable()
    {
        EventHandler.CloseUI -= OnCloseUI;
        EventHandler.OpenStorePanel -= OnOpenStorePanel;
        EventHandler.BuyItem -= OnBuyItem;
        EventHandler.CorrectBuyItem -= OnCorrectBuyItem;
        EventHandler.CancelBuyItem -= OnCancelBuyItem;
    }

    private void OnOpenStorePanel()
    {
        storePanel.SetActive(true);
    }

    private void OnCloseUI()
    {
        storePanel.SetActive(false);
    }

    private void OnCorrectBuyItem(BuyItemDetails data)
    {
        // Check have money to buy item
        if (GameManager.Instance.currentPlayerMoney > data.itemPrice)
        {
            // Buy successful, switch window to "CorrectWindow"
            EventHandler.CallBuyItemSuccessful(data);
            StartCoroutine(SwitchWindow(checkPanelWindow, buyCorrectWindow, 0));
            StartCoroutine(SwitchWindow(buyCorrectWindow, mainStoreWindow, 1));

            Debug.Log(data.itemPrice);
            GameManager.Instance.currentPlayerMoney -= data.itemPrice;
            GameManager.Instance.spendMoneyCount += data.itemPrice;
        }
        else
        {
            buyFailedMessageText.text = "餘額不足";
            StartCoroutine(SwitchWindow(checkPanelWindow, buyFailedWindow, 0));
            StartCoroutine(SwitchWindow(buyFailedWindow, mainStoreWindow, 1));
        }
        
        UpdateUI();
    }

    private void OnCancelBuyItem()
    {
        StartCoroutine(SwitchWindow(checkPanelWindow, mainStoreWindow, 0));
    }

    private void OnBuyItem(BuyItemDetails data)
    {
        StartCoroutine(SwitchWindow(mainStoreWindow, checkPanelWindow, 0));
    }

    #endregion

    private void StartLoadingBarAnimation()
    {
        loadingBar.DOValue(1, loadingBarValueFadeTime).OnComplete(() =>
        {
            StartCoroutine(SwitchWindow(loadingBarParent, mainStoreWindow, 0));
            EventHandler.CallStorePanelLoadingDone();
        });
    }

    private void UpdateUI()
    {
        currentMoneyText.text = GameManager.Instance.currentPlayerMoney.ToString();
    }

    IEnumerator SwitchWindow(CanvasGroup startWindow, CanvasGroup targetWindow, float waitToStartTime)
    {
        yield return new WaitForSeconds(waitToStartTime);
        
        startWindow.gameObject.SetActive(true);
        targetWindow.gameObject.SetActive(false);
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(startWindow.DOFade(0, panelFadeTime).OnComplete(() =>
        {
            startWindow.gameObject.SetActive(false);
            targetWindow.gameObject.SetActive(true);
        }));
        sequence.Append(targetWindow.DOFade(1, panelFadeTime));
        yield return null; 
    }
}
