using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHealth : BaseHealth
{
    [Header("Self Component")]
    public GameObject parent;
    
    protected override void Dead()
    {
        Destroy(parent);
    }
}
