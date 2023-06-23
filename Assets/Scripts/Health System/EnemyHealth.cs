using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyHealth : BaseHealth
{
    [SerializeField] private Slider healthBar;
    private BaseEnemy baseEnemy => GetComponent<BaseEnemy>();

    //[Header("Setting")] 
    //public GameObject enemyDeadVFX;
    
    public override void Damage(int damage)
    {
        base.Damage(damage);
        
        // Health Bar
        healthBar.value = (float)currentHealth / maxHealth;
    }

    protected override void Dead()
    {
        // Play dead animation
        float angle = Random.Range(0, 360);
        Instantiate(deadVFX, transform.position, quaternion.Euler(0, 0, angle));
        Destroy(gameObject);
    }
}
