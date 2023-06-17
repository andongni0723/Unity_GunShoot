using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    [Header("Component")]
    public GameObject headLeftPoint;
    public GameObject headCenterPoint;
    public GameObject headRightPoint;

    [Header("Setting")] 
    public bool isDraw;
    public LayerMask hitLayer;

    [Header("Debug")] 
    [SerializeField] private CharacterHeadState playerHeadState;
    
    private RaycastHit2D[] leftHits = new RaycastHit2D[5];
    private RaycastHit2D[] centerHits = new RaycastHit2D[5];
    private RaycastHit2D[] rightHits = new RaycastHit2D[5];

    private void OnDrawGizmos()
    {
        if (isDraw)
        {
            Debug.DrawRay(transform.position, Vector2.up, Color.green);
            Gizmos.DrawRay(headLeftPoint.transform.position, transform.up * 3);
            Gizmos.DrawRay(headCenterPoint.transform.position, transform.up * 3);
            Gizmos.DrawRay(headRightPoint.transform.position, transform.up * 3);
        }
    }

    private void Update()
    {
        int leftHitCount   = Physics2D.RaycastNonAlloc(headLeftPoint.transform.position, transform.up, leftHits, 3, hitLayer);
        int centerHitCount = Physics2D.RaycastNonAlloc(headCenterPoint.transform.position, transform.up, centerHits, 3, hitLayer);
        int rightHitCount  = Physics2D.RaycastNonAlloc(headRightPoint.transform.position, transform.up, rightHits, 3, hitLayer);

        for (int i = 0; i < centerHitCount; i++)
        {
            Debug.Log($"{centerHits[i].transform.name}, {Vector2.Distance(transform.position, centerHits[i].transform.position)}");
            break;
        }

        // Execute Head State Action
        switch (playerHeadState)
        {
            case CharacterHeadState.Left:
                //transform.position = headLeftPoint.transform.position;
                transform.DOMove(headLeftPoint.transform.position, 0.2f);
                break;
            
            case CharacterHeadState.Center:
                //transform.position = headCenterPoint.transform.position;
                transform.DOMove(headCenterPoint.transform.position, 0.2f);

                break;
            
            case CharacterHeadState.Right:
                //transform.position = headRightPoint.transform.position;
                transform.DOMove(headRightPoint.transform.position, 0.2f);
                break;
        }
        
        // Change Player Head Position State
        if (leftHitCount > 0 && centerHitCount > 0 && rightHitCount > 0)
        {
            playerHeadState = CharacterHeadState.Center;

        }
        else if (centerHitCount > 0 && rightHitCount > 0)
        {
            playerHeadState = CharacterHeadState.Left;
        }
        else if(leftHitCount > 0 && centerHitCount > 0)
        {
            playerHeadState = CharacterHeadState.Right;
        }
        else
        {
            playerHeadState = CharacterHeadState.Center;
        }
        
    }
}
