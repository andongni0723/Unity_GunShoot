using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class HealthChangeTextVFX : MonoBehaviour
{
    [Header("Component")] 
    public TextMeshProUGUI healthText;
    public GameObject textMoveTargetPoint;
    [SerializeField] private CanvasGroup canvasGroup => GetComponent<CanvasGroup>();

    [Header("Setting")] 
    public Color increaseHealthTextColor;
    public Color decreaseHealthTextColor;

    [Space(15f)] 
    public float textMoveTime = 1;

    
    /// <summary>
    /// The method will execute when BaseHealth call
    /// </summary>
    /// <param name="changeValue"></param>
    /// <returns></returns>
    public void CallChangeHealthTextAnimation(int changeValue)
    {
        StartCoroutine(ChangeHealthTextAnimation(changeValue));
    }

    private IEnumerator ChangeHealthTextAnimation(int changeValue)
    {
        // Update Display UI
       
        if (changeValue > 0) // increase health
        {
            healthText.text = $"+{changeValue}";
            healthText.color = increaseHealthTextColor;
        }
        else // decrease health
        {
            healthText.text = changeValue.ToString();
            healthText.color = decreaseHealthTextColor;
        }

        // Animation Start
        Sequence sequence = DOTween.Sequence();
        
        //sequence.Append(transform.DOMoveY(textMoveTargetPoint.transform.position.y, textMoveTime));
        sequence.Append(healthText.gameObject.transform.DOMove(textMoveTargetPoint.transform.position, textMoveTime));
        sequence.Join(canvasGroup.DOFade(0, textMoveTime));
        sequence.OnComplete(() => Destroy(gameObject));
        
        yield return null;
    }
}
