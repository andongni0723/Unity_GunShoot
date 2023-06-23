using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryVFX : MonoBehaviour
{
    private Action<RecoveryVFX> spawnAction;
    private void OnEnable()
    {
        StartCoroutine(TimeToAction());
    }
    
    public void SetSpawnAction(Action<RecoveryVFX> spawnAction)
    {
        this.spawnAction = spawnAction;
    }

    IEnumerator TimeToAction()
    {
        yield return new WaitForSeconds(0.3f);
        
        spawnAction?.Invoke(this);
    }
}
