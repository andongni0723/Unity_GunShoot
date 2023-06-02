using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceActive : MonoBehaviour
{
    public float destroyTime = 5;
    private Action<TraceActive> spawnAction;
    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
    
    // Pool action
    public void SetSpawnAction(Action<TraceActive> spawnAction)
    {
        this.spawnAction = spawnAction;
    }
}