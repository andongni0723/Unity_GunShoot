using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Action<Ball> spawnAction;
    private void Start()
    {
        StartCoroutine(DestroyObject());
    }
    

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1);
        spawnAction?.Invoke(this);
    }

    public void SetSpawnAction(Action<Ball> spawnAction)
    {
        this.spawnAction = spawnAction;
    }
}
