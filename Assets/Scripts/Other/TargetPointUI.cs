using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TargetPointUI : MonoBehaviour
{
    private RectTransform _rectTrans => GetComponent<RectTransform>();

    [Header("Component")]
    public TextMeshProUGUI distanceText;
    private Canvas _canvas => GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

    [Header("Setting")]
    public Transform targetTransform;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => targetTransform != default);
        
        CinemachineCore.CameraUpdatedEvent.AddListener(CameraUpdateUI); 
    }

    private void OnDestroy()
    {
        CinemachineCore.CameraUpdatedEvent.RemoveListener(CameraUpdateUI);
    }

    private void CameraUpdateUI(CinemachineBrain arg0)
    {
        _rectTrans.position = GetClampPos(RectTransformUtility.WorldToScreenPoint(Camera.main, targetTransform.position), 
            CalRectByVector2(new Vector2(Screen.width, Screen.height), _rectTrans.sizeDelta));
            
        distanceText.text = $"{Vector2.Distance(GameManager.Instance.playerObject.transform.position, targetTransform.position) * 5:00}m";
    }

    private Vector2 GetClampPos(Vector2 pos, Rect area)
    {
        Vector2 safePos = Vector2.zero;
        safePos.x = Mathf.Clamp(pos.x, area.xMin, area.xMax);
        safePos.y = Mathf.Clamp(pos.y, area.yMin, area.yMax);

        return safePos;
    }

    private Rect CalRectByVector2(Vector2 cameraSize, Vector2 uiSize)
    {
        Rect rect = Rect.zero;

        //减去uiSize的一半是为了防止UI元素一般溢出屏幕
        rect.xMax = cameraSize.x - uiSize.x * 1.5f;
        rect.yMax = cameraSize.y - uiSize.y * 1.5f;
        rect.xMin = uiSize.x;
        rect.yMin = uiSize.y;

        return rect;
    }

    
    /// <summary>
    /// Will call by Parent( MissionUI )
    /// </summary>
    public void MissionCompleteDestroy()
    {
        CinemachineCore.CameraUpdatedEvent.RemoveListener(CameraUpdateUI);
    }
}
