using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public int speed;
    private Action<Bullet> spawnAction;
    private BulletFirePool bulletFirePool;

    private void Awake()
    {
        bulletFirePool = GameObject.FindWithTag("PoolManager").GetComponent<BulletFirePool>();
    }

    private void OnEnable()
    {
        StartCoroutine(TimeToAction());
        GetComponent<TrailRenderer>().Clear();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.up * (speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        bulletFirePool.FireVFX(transform.position, transform.rotation);
        spawnAction?.Invoke(this);
    }

    public void SetSpawnAction(Action<Bullet> spawnAction)
    {
        this.spawnAction = spawnAction;
    }

    IEnumerator TimeToAction()
    {
        yield return new WaitForSeconds(3);
        
        spawnAction?.Invoke(this);
    }
}
