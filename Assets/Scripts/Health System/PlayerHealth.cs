using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerHealth : BaseHealth
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private CanvasGroup playerHurtBloodPanelVFX;
    
    private PlayerController player => GetComponent<PlayerController>();

    // [Header("Setting")]
    // public GameObject playerDeadVFX;
    
    public override void Damage(int damage)
    {
        base.Damage(damage);
        
        // Red Blood VFX
        Sequence sequence = DOTween.Sequence();
        sequence.Append(playerHurtBloodPanelVFX.DOFade(1, 0.1f));
        sequence.Append(playerHurtBloodPanelVFX.DOFade(0, 0.8f));
        CameraController.Instance.CameraShake(10, 0.1f);
        
        // Health Bar
        healthBar.value = (float)currentHealth / maxHealth;
        
        // GameManager update details
        GameManager.Instance.spendHpCount += damage;
    }

    public override void Recovery(int increaseValue)
    {
        base.Recovery(increaseValue);
        
        // Health Bar
        healthBar.value = (float)currentHealth / maxHealth; 
    }

    protected override void Dead()
    {
        // Play dead animation
        float angle = player.player.transform.rotation.x; // Get player sprite rotation
        Instantiate(deadVFX, transform.position, quaternion.Euler(0, 0, angle));
        Debug.Log(angle);
        gameObject.SetActive(false);
        
        EventHandler.CallPlayerDead();
    }
}
