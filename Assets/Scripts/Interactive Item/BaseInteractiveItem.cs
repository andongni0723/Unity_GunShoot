using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractiveItem : MonoBehaviour
{
    public string interactiveDescription; // The player close to item, the player UI will show button and text from this string.
    public abstract string InteractiveDescription { get; set; }

    private void Awake()
    {
        InteractiveDescription = interactiveDescription;
    }

    /// <summary>
    /// Children Scripts will add something in there
    /// </summary>
    public abstract void InteractiveAction();
}
