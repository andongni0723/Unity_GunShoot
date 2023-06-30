using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractiveItem : MonoBehaviour
{
    public string interactiveDescription; // The player close to item, the player UI will show button and text from this string.
    
    #region Event

    private void OnEnable()
    {
        EventHandler.InteractiveItem += OnInteractiveItem; // Execute interactive action
    }

    private void OnDisable()
    {
        EventHandler.InteractiveItem -= OnInteractiveItem;
    }
    
    private void OnInteractiveItem(GameObject targetItem)
    {
        if (gameObject == targetItem)
        {
            InteractiveAction(); 
        }
    }
    
    #endregion 
    
    
    /// <summary>
    /// Children Scripts will add something in there
    /// </summary>
    protected virtual void InteractiveAction() { }
}
