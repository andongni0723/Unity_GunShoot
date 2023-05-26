using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : BaseHealth
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private CanvasGroup canvasGroup;
    public override void Damage(int damage)
    {
        base.Damage(damage);
        
        // Red Blood VFX
        Sequence sequence = DOTween.Sequence();
        sequence.Append(canvasGroup.DOFade(1, 0.1f));
        sequence.Append(canvasGroup.DOFade(0, 0.8f));
        CinemachineShake.Instance.CameraShake(10, 0.1f);
        
        // Health Bar
        healthBar.value = (float)currentHealth / maxHealth;
    }
}
