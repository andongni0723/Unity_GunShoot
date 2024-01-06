using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPoint : MonoBehaviour
{
    public GameObject TargetPointUIPrefabs;
    public MissionUI parentMission;
    public Transform targetPointParent;
    private GameObject _canvas => GameObject.FindGameObjectWithTag("Canvas");
    private TargetPointUI _newTargetPoint;

    [Header("Setting")] 
    public bool isItemRealPoint;

    private void Start()
    {
        
        targetPointParent = GameObject.FindGameObjectWithTag("CanvasPoint").transform;
        // Instantiate Target Point UI show to Player
        if (!isItemRealPoint)
        {
            _newTargetPoint = Instantiate(TargetPointUIPrefabs, _canvas.transform).GetComponent<TargetPointUI>();
            
            // Set UI order in Hierarchy
            // Canvas
            // |- ...
            // |- Point <- targetPointParent
            // |- (targetPoint)
            // |- ...
            _newTargetPoint.transform.SetSiblingIndex(targetPointParent.GetSiblingIndex() + 1);
            
            _newTargetPoint.targetTransform = transform; 
        }
    }

    private void OnDestroy()
    {
        // Destroy the target point UI
        if(_newTargetPoint != null)
            Destroy(_newTargetPoint.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player Gone on Mission Point 
        if (other.CompareTag("Player"))
        {
            // If player gone point is item real point 
            if(isItemRealPoint)
                parentMission.isPlayerArrivesRealItemPoint = true;
            else
                parentMission.playerArrivesTargetPointCount++;
            
            if(_newTargetPoint != null)
                Destroy(_newTargetPoint.gameObject);
            
            Destroy(gameObject);
        }
    }
}
