using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class ScoutBomb : ThrowItem
{
    private TraceObjectPool traceObjectPool => GameObject.FindWithTag("PoolManager").GetComponent<TraceObjectPool>();
    public GameObject scoutLightVFX;
    public GameObject enemyTracePrefabs;
    public float lightVFXRotationSpeed = 3;
    public bool isAction = false;

    private void Update()
    {
        if (isAction)
        {
            ScoutLightVFXRotation();
        }
    }

    public override void Throw(Vector2 startForce)
    {
        base.Throw(startForce);
        scoutLightVFX.SetActive(false);
        AudioManager.Instance.PlayItemAudio(AudioManager.Instance.grenadeThrowAudio);
    }

    protected override void Action()
    {
        base.Action();
        scoutLightVFX.SetActive(true);
        AudioManager.Instance.PlayItemAudio(AudioManager.Instance.scoutAudio);
        isAction = true;
    }

    //this method will check area enemy and trace
    protected override void CheckHurt()
    {
        // Check area have enemy
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, scoutLightVFX.GetComponent<Light2D>().pointLightOuterRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                // Set trace object 
                traceObjectPool.TraceObject(hit.transform.position);
                
                hit.GetComponent<BaseHealth>().Damage(damage);
            }
        }
    }

    private void ScoutLightVFXRotation()
    {
        scoutLightVFX.transform.Rotate(0, 0, lightVFXRotationSpeed * Time.deltaTime);
    }
}
