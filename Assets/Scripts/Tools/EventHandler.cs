using System;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static event Action PlayerDead;
    public static void CallPlayerDead()
    {
        PlayerDead?.Invoke();
    }
}
