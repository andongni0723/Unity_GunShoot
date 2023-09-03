using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : BaseInteractiveItem
{
    public override string InteractiveDescription { get; set; }

    protected override void InteractiveAction()
    {
        EventHandler.CallOpenStorePanel();
    }
}
