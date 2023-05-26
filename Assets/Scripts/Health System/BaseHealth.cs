using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseHealth : MonoBehaviour
{
    public int currentHealth = 100;
    public int maxHealth = 100;

    public virtual void Damage(int damage)
    {
        currentHealth -= damage;
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
