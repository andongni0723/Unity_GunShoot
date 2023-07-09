using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHit : BaseHealth
{
    private ShieldHitVFXPool shieldHitVFXPool =>
        GameObject.FindWithTag("PoolManager").GetComponent<ShieldHitVFXPool>();
    
    public override void Damage(int damage)
    {
        //abase.Damage(damage);
        AudioManager.Instance.PlayItemAudio(AudioManager.Instance.shieldHitAudio);
        shieldHitVFXPool.PlayHitVFX(transform.position, transform.rotation);
    }
}
