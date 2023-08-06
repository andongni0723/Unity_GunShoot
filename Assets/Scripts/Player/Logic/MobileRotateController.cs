using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class MobileRotateController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float prePosX;
    [SerializeField] private float currentPosX;
    
    public float rotationZ;
    public void OnBeginDrag(PointerEventData eventData)
    {
        prePosX = eventData.position.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentPosX = eventData.position.x;

        rotationZ -= (currentPosX - prePosX) * 20 * Time.deltaTime;
        
        prePosX = currentPosX;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
