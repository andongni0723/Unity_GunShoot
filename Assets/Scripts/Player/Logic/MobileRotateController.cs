using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileRotateController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float prePosX;
    [SerializeField] private float currentPosX;

    public float rZ = 0;
    public void OnBeginDrag(PointerEventData eventData)
    {
        prePosX = eventData.position.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        currentPosX = eventData.position.x;

        //player.transform.rotation = Quaternion.Euler(0, 0, 
            //player.transform.eulerAngles.z - (currentPosX - lastPosX) * 20);

        rZ = rZ - (currentPosX - prePosX) * 20 * Time.deltaTime;
        
        
        prePosX = currentPosX;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
