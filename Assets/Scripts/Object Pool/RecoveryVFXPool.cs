using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryVFXPool : BasePool<RecoveryVFX>
{
    private void Awake()
    {
        Initialize();
    }

    public void RecoveryVFX(Vector3 position, Quaternion rotation)
    {
        var obj = Get();

        obj.transform.position = position;
        obj.transform.rotation = rotation;
    }
    
    protected override RecoveryVFX OnCreatePoolItem()
    {
        var recoveryVFX = base.OnCreatePoolItem();
        recoveryVFX.SetSpawnAction(delegate { Release(recoveryVFX); });
        return recoveryVFX;
    }
}
