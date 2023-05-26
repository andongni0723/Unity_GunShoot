using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : BaseHealth
{
    [SerializeField] private Slider healthBar;
    private BaseEnemy baseEnemy => GetComponent<BaseEnemy>();

    [Header("Setting")] 
    public GameObject EnemyDeadVFX;
    
    public override void Damage(int damage)
    {
        base.Damage(damage);
        
        // Health Bar
        healthBar.value = (float)currentHealth / maxHealth;
    }

    protected override void Dead()
    {
        // Play dead animation
        float angle = -baseEnemy.enemyObject.transform.rotation.z * Mathf.Rad2Deg;
        Instantiate(EnemyDeadVFX, transform.position, Quaternion.Euler(0, 0, angle));
        Debug.Log(angle);
        Destroy(gameObject);
    }
}
