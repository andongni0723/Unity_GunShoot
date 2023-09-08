using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : BaseInteractiveItem
{
    public override string InteractiveDescription { get; set; }

    public override void InteractiveAction()
    {
        EventHandler.CallGameWin();
    }
}
