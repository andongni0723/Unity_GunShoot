using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : BasePool<Bullet>
{
    private void Awake()
    {
        Initialize();
    }

    public void Fire(Vector3 position, Quaternion rotation, int damage)
    {
        var obj = Get();

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.GetComponent<Bullet>().damage = damage;
        obj.GetComponent<TrailRenderer>().Clear();
    }
    
    protected override Bullet OnCreatePoolItem()
    {
        var bullet = base.OnCreatePoolItem();
        bullet.SetSpawnAction(delegate { Release(bullet); });
        return bullet;
    }
}
