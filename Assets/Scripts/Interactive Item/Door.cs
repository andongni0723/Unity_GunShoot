using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Door : BaseInteractiveItem
{
    [Header("Self Component")] 
    public GameObject doorAxisObj;

    [Header("Self Setting")] 
    public float doorActionTime = 1;
    
    [SerializeField]private bool isOpen;
    
    public override string InteractiveDescription { get; set; }

    public override void InteractiveAction()
    {
        OpenOrCloseDoor();
    }

    private void OpenOrCloseDoor()
    {
        isOpen = !isOpen;
        InteractiveDescription = isOpen ? "關門" : "開門";

        doorAxisObj.transform.DOLocalRotateQuaternion(quaternion.Euler(0, 0, isOpen? -90 : 0), doorActionTime);
    }
    
    
}
