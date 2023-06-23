using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseHealth : MonoBehaviour
{
    public int currentHealth = 100;
    public int maxHealth = 100;
    [Range(0, 100)] public int defense = 0;

    [SerializeField] private Transform worldCanvas => GameObject.FindWithTag("WorldCanvas").transform;
    
    [Header("Component")] 
    public GameObject deadVFX;
    public GameObject TextVFXInstantiatePoint;
    public GameObject healthChangeTextVFXObject;
    public RecoveryVFXPool recoveryVFXPool; 
        
    public virtual void Damage(int damage)
    {
        currentHealth -= (int)(damage * (1 - defense * 0.01f));
        
        // Animation
        if (damage != 0)
        {
            GameObject textVFX = Instantiate(healthChangeTextVFXObject,
                    TextVFXInstantiatePoint.transform.position, Quaternion.identity, worldCanvas);
                    
            textVFX.GetComponent<HealthChangeTextVFX>().CallChangeHealthTextAnimation(damage * -1); 
        }
       
        
        CheckDead();
    }

    public virtual void Recovery(int increaseValue)
    {
        // Check health recovery is bigger than max health
        increaseValue = maxHealth - currentHealth >= increaseValue ? increaseValue : maxHealth - currentHealth;
        currentHealth += increaseValue;
        
        // Animation
        if (increaseValue != 0)
        {
            GameObject textVFX = Instantiate(healthChangeTextVFXObject,
                TextVFXInstantiatePoint.transform.position, Quaternion.identity, worldCanvas);
 
             textVFX.GetComponent<HealthChangeTextVFX>().CallChangeHealthTextAnimation(increaseValue);
        }
 
        // VFX
        recoveryVFXPool.RecoveryVFX(transform.position, Quaternion.identity);
        

    }

    private void CheckDead()
    {
        if (currentHealth <= 0)
        {
           Dead();
        }
    }

    protected virtual void Dead()
    {
    }
}
