using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceObjectPool : BasePool<TraceActive>
{
    private void Awake()
    {
        Initialize();
    }

    public void TraceObject(Vector3 position)
    {
        var obj = Get();
        obj.transform.position = position;
    }
    
    protected override TraceActive OnCreatePoolItem()
    {
        var traceActive = base.OnCreatePoolItem();
        traceActive.SetSpawnAction(delegate { Release(traceActive); });
        return traceActive;
    }
}
