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
    
    protected override void InteractiveAction()
    {
        OpenOrCloseDoor();
    }

    private void OpenOrCloseDoor()
    {
        isOpen = !isOpen;
        interactiveDescription = isOpen ? "關門" : "開門";

        doorAxisObj.transform.DOLocalRotateQuaternion(quaternion.Euler(0, 0, isOpen? -90 : 0), doorActionTime);
    }
    
    
}
