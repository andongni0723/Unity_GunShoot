using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShieldHitVFX: MonoBehaviour
{
    private Action<TraceActive> spawnAction;
    
    [Header("Setting")]
    public Vector3 endScale;
    public float time;
    
    private void Start()
    { 
        //Destroy(gameObject, time);
        Hit();
    }

    public void Hit()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(endScale, time)); // to big
        sequence.Join(GetComponent<SpriteRenderer>().DOFade(0, time)); // fade to disappear
    }
    
    // Pool action
    public void SetSpawnAction(Action<TraceActive> spawnAction)
    {
        this.spawnAction = spawnAction;
    }
}
