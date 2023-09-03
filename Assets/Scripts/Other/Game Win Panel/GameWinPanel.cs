using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameWinPanel : MonoBehaviour
{
    [Header("Component")] 
    public GameObject panelObj;
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI scoreText;
    public Slider scoreSlider;
    
    [Space(20)]
    public GameWinDetailsUI killEnemyDetailsUI;
    public GameWinDetailsUI spendHpDetailsUI;
    public GameWinDetailsUI spendMoneyDetailsUI;

    [Header("Var")] 
    [SerializeField] private int score;

    [Header("Setting")] 
    public float animationTime = 1;
    
    private const int MAX_SPEND_HP_SCORE_PRICE = 1000;
    private const int MAX_SPEND_MONEY_SCORE_PRICE = 600;

    #region Event

    private void OnEnable()
    {
        EventHandler.GameWinCameraSwitchDone += OnGameWinCameraSwitchDone; // Game win panel animation 
    }

    private void OnDisable()
    {
        EventHandler.GameWinCameraSwitchDone -= OnGameWinCameraSwitchDone;
    }

    private void OnGameWinCameraSwitchDone()
    {
        WinPanelINIT();
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1, animationTime));
        sequence.Join(scoreSlider.DOValue(score / CountScore(GameManager.Instance.maxEnemyCount, 0, 0), animationTime));

        StartCoroutine(GameDetailsShowAnimationAction());
    }

    #endregion

    private IEnumerator GameDetailsShowAnimationAction()
    {
        // Show score number animation 
        yield return StartCoroutine(ScoreNumberAnimation(score, scoreText));
        
        //show details number animation
        yield return StartCoroutine(ScoreNumberAnimation(GameManager.Instance.killEnemyCount, killEnemyDetailsUI.detailsValueText));
        yield return StartCoroutine(ScoreNumberAnimation(GameManager.Instance.spendHpCount, spendHpDetailsUI.detailsValueText));
        yield return StartCoroutine(ScoreNumberAnimation(GameManager.Instance.spendMoneyCount, spendMoneyDetailsUI.detailsValueText)); 
    }

    private IEnumerator ScoreNumberAnimation(float showNumber, TextMeshProUGUI targetText, int numberUpdateFrame = 100)
    {
        // 1 second add 100 number
        float _count = showNumber / numberUpdateFrame;
        float _currentScore = 0;

        // if (_count == 0 && showNumber != 0)
        //     _currentScore = showNumber;
        
        for (int i = 0; i < numberUpdateFrame; i++)
        {
            _currentScore += _count;
            targetText.text = _currentScore.ToString("0");
            yield return new WaitForSeconds(animationTime / (float)numberUpdateFrame);
        }

        targetText.text = showNumber.ToString("0");
        yield return null;
    }

    private void WinPanelINIT()
    {
        score = (int)CountScore(GameManager.Instance.killEnemyCount, GameManager.Instance.spendHpCount, GameManager.Instance.spendMoneyCount);
        canvasGroup.alpha = 0;
        scoreSlider.value = 0;
        panelObj.SetActive(true);
    }
    
    private float CountScore(int killEnemyCount, int spendHpCount, int spendMoneyCount)
    {
        return killEnemyCount * (
            Mathf.Max(0, MAX_SPEND_HP_SCORE_PRICE - spendHpCount) * 100 +
            Mathf.Max(0, MAX_SPEND_MONEY_SCORE_PRICE - spendMoneyCount) * 80);
    }
    
}
