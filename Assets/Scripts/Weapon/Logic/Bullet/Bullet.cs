using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public int damage;
    public int speed;
    private Action<Bullet> spawnAction;
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private BulletFirePool bulletFirePool => GameObject.FindWithTag("PoolManager").GetComponent<BulletFirePool>();

    private void FixedUpdate()
    {
        Move();
    }

    private void OnEnable()
    {
        StartCoroutine(TimeToAction());
        //Debug.Break();
        GetComponent<TrailRenderer>().Clear();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check Can Damage
        if (other.transform.TryGetComponent<BaseHealth>(out var baseHealth))
        {
            baseHealth.Damage(damage);
        }
        
        // Play VFX
        bulletFirePool.FireVFX(transform.position, transform.rotation);
        spawnAction?.Invoke(this);
    }

    private void Move()
    {
        //transform.Translate(Vector3.up * (speed * Time.deltaTime));
        //rb.MovePosition(transform.position + Vector3.up * (speed * Time.deltaTime));
        //rb.velocity = new Vector2(0, speed * Time.deltaTime);
        rb.velocity = transform.up * speed;
        //Debug.Log("move");
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
