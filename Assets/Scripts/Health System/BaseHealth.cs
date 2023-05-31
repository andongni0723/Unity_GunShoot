using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseHealth : MonoBehaviour
{
    public int currentHealth = 100;
    public int maxHealth = 100;
    [Range(0, 90)] public int defense = 0;

    public virtual void Damage(int damage)
    {
        currentHealth -= (int)(damage * (1 - defense * 0.01f));
        CheckDead();
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
