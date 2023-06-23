using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class TargetPointUI : MonoBehaviour
{
    private RectTransform rectTrans => GetComponent<RectTransform>();

    //public CinemachineVirtualCamera camera;
    public TextMeshProUGUI distanceText;
    public Canvas canvas;
    public Transform targetTransform;

    private void Start()
    {
        CinemachineCore.CameraUpdatedEvent.AddListener(delegate
        {
            //rectTrans.position = GetClampPos(RectTransformUtility.WorldToScreenPoint(Camera.main, targetTransform.position), CalRectByCanvas(canvas, rectTrans.sizeDelta));
            rectTrans.position = GetClampPos(RectTransformUtility.WorldToScreenPoint(Camera.main, targetTransform.position), 
                CalRectByVector2(new Vector2(Screen.width, Screen.height), rectTrans.sizeDelta));
            
            distanceText.text = $"{(Vector2.Distance(GameManager.Instance.playerObject.transform.position, targetTransform.position) * 5).ToString("00")}m";
        }); 
    }

    private Vector2 GetClampPos(Vector2 pos, Rect area)
    {
        Vector2 safePos = Vector2.zero;
        safePos.x = Mathf.Clamp(pos.x, area.xMin, area.xMax);
        safePos.y = Mathf.Clamp(pos.y, area.yMin, area.yMax);

        return safePos;
    }

    private Rect CalRectByCanvas(Canvas c, Vector2 uiSize)
    {
        Rect rect = Rect.zero;
        Vector2 area = c.GetComponent<RectTransform>().sizeDelta;

        //减去uiSize的一半是为了防止UI元素一般溢出屏幕
        // rect.xMax = area.x - uiSize.x / 2;
        // rect.yMax = area.y - uiSize.y / 2;
        // rect.xMin = uiSize.x / 2;
        // rect.yMin = uiSize.y / 2;
        rect.xMax = area.x - uiSize.x;
        rect.yMax = area.y - uiSize.y;
        rect.xMin = uiSize.x;
        rect.yMin = uiSize.y;

        return rect;
    }
    
    private Rect CalRectByVector2(Vector2 cameraSize, Vector2 uiSize)
    {
        Rect rect = Rect.zero;

        //减去uiSize的一半是为了防止UI元素一般溢出屏幕
        // rect.xMax = area.x - uiSize.x / 2;
        // rect.yMax = area.y - uiSize.y / 2;
        // rect.xMin = uiSize.x / 2;
        // rect.yMin = uiSize.y / 2;
        rect.xMax = cameraSize.x - uiSize.x;
        rect.yMax = cameraSize.y - uiSize.y;
        rect.xMin = uiSize.x;
        rect.yMin = uiSize.y;

        return rect;
    }
}
