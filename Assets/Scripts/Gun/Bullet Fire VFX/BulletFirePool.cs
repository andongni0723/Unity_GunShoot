using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFirePool : BasePool<BulletFire>
{
    private void Awake()
    {
        Initialize();
    }

    public void FireVFX(Vector3 position, Quaternion rotation)
    {
        var obj = Get();

        obj.transform.position = position;
        obj.transform.rotation = rotation;
    }
    
    protected override BulletFire OnCreatePoolItem()
    {
        var bulletFire = base.OnCreatePoolItem();
        bulletFire.SetSpawnAction(delegate { Release(bulletFire); });
        return bulletFire;
    }
}
