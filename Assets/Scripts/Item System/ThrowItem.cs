using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ThrowItem : MonoBehaviour
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();

    [Header("Setting")]
    //public Vector2 startForce;
    public float force = 1;
    public float toActionTime = 1.7f;
    public float destroyTime = 3;
    public int damage = 100;

    public virtual void Throw(Vector2 startForce)
    {
        rb.AddForce(startForce.normalized * force, ForceMode2D.Impulse);
        Invoke(nameof(Action), toActionTime);
        
    }

    protected virtual void Action()
    {
        Destroy(gameObject, destroyTime);
        CheckHurt();
    }

    protected virtual void CheckHurt()
    {
        
    }
}
