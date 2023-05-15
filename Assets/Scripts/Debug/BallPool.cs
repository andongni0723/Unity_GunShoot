using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Pool;

public class BallPool : BasePool<Ball>
{
    private void Awake()
    {
        Initialize(); 
    }

    private void Update()
    {
        var ball = Get();
        ball.transform.position = transform.position;
    }

    protected override Ball OnCreatePoolItem()
    {
        var ball =  base.OnCreatePoolItem();
        ball.SetSpawnAction(delegate { Release(ball); });
        return ball;
    }

    protected override void OnGetPoolItem(Ball obj)
    {
        base.OnGetPoolItem(obj);
        obj.transform.position = transform.position;
    }
}
