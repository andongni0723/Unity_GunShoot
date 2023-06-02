using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShieldHitVFXPool : BasePool<ShieldHitVFX>
{
    private void Awake()
    {
        Initialize();
    }

    public void PlayHitVFX(Vector3 position, Quaternion rotation)
    {
        var obj = Get();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        GetComponent<SpriteRenderer>().DOFade(1, 0);
        //transform.localScale = new Vector3(8, 0.5f, 1);
    }
    
    protected override ShieldHitVFX OnCreatePoolItem()
    {
        var traceActive = base.OnCreatePoolItem();
        traceActive.SetSpawnAction(delegate { Release(traceActive); });
        return traceActive;
    }
}
