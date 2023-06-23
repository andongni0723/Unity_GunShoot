using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : BaseInteractiveItem
{
    protected override void InteractiveAction()
    {
        EventHandler.CallOpenStorePanel();
    }
}
