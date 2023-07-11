using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionCountItem : MonoBehaviour
{
    [Header("Component")] 
    public MissionUI parentMission;
    
    private void OnDestroy()
    {
        parentMission.itemDisappearCount++;
    }
}
