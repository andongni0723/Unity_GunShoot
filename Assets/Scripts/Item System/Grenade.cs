using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : ThrowItem
{
    [Header("Component")] public GameObject BombVFX;
    public float bombArea;
    public LayerMask rayLayerMask;
    
    private bool isAction = false;

    public override void Throw(Vector2 startForce)
    {
        //Setting
        BombVFX.SetActive(false);
        base.Throw(startForce);
        AudioManager.Instance.GrenadeAudio(AudioManager.Instance.grenadeThrowAudio);
    }

    protected override void Action()
    {
        base.Action();
        // isAction = true;
        BombVFX.SetActive(true);
        AudioManager.Instance.GrenadeAudio(AudioManager.Instance.grenadeBombAudio);

        // Invoke(nameof(CloseAction), 1);
    }

    // private void CloseAction() => isAction = true;
    //
    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     if (isAction)
    //     {
    //         CheckHurt(other.gameObject);
    //         isAction = false;
    //     }
    // }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, bombArea);
    }

    protected override void CheckHurt()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, bombArea);

        foreach (var hit in hits)
        {
            //Debug.Log("hit1");
            CheckTargetBlock(hit); 
        }
    }

    private void CheckTargetBlock(Collider2D target)
    {
        //playerPos = playerPosUpdate;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, target.transform.position - transform.position,
            Vector2.Distance(transform.position, target.transform.position) + 10, rayLayerMask);

        if (hits.Length != 0)
        {
            Debug.Log($"{hits[0].transform.name}");
            // Check first hit object is target, that is be self can see target
            if (hits[0].transform.name == target.transform.name)
            {
                // Hurt Damage
                if (target.gameObject.TryGetComponent<BaseHealth>(out var baseHealth))
                {
                    baseHealth.Damage(damage);
                    Debug.Log($"hit, {hits.Length} {hits[0].transform.name}");
                } 
            }
        }
    }
}
