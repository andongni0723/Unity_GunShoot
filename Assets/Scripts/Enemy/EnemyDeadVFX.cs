using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadVFX : MonoBehaviour
{
    public float time = 2;
    private void Start()
    {
        Invoke("DestroySelf", time);
        //StartCoroutine(TimeToDestroy(time));
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
    IEnumerator TimeToDestroy(float _time)
    {
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);
    }
}
