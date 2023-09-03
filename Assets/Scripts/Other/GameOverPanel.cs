using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();
    
    [Header("Component")]
    public GameObject panelObj;
    public TextMeshProUGUI lostText;
    public Button againButton;

    [Header("Setting")] 
    public float canvasActiveFadeTime;
    public float canvasColorFadeTime;
    
    private void Start()
    {
        panelObj.SetActive(false);
        lostText.gameObject.SetActive(false);
        panelObj.GetComponent<Image>().color = Color.white;
        canvasGroup.alpha = 0;
        againButton.gameObject.SetActive(false);
    }

    #region Event
    private void OnEnable()
    {
        EventHandler.PlayerDead += OnPlayerDead;
    }

    private void OnDisable()
    {
        EventHandler.PlayerDead -= OnPlayerDead;
    }

    private void OnPlayerDead()
    {
        panelObj.SetActive(true);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1, canvasActiveFadeTime)
            .OnComplete(() => { lostText.gameObject.SetActive(true); }));
        sequence.Append(panelObj.GetComponent<Image>().DOColor(Color.black, canvasColorFadeTime));
        sequence.OnComplete(AfterAnimation);
    }
    #endregion

    private void AfterAnimation()
    {
        againButton.gameObject.SetActive(true);
    }
}

