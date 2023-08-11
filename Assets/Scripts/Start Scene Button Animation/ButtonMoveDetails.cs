using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ButtonMoveDetails : MonoBehaviour
{
    [Header("Setting")]
    public Vector2 originPosition;
    public Vector2 movePosition;
    public float moveSpeed = 0.5f;
    
    
    public Tween ToMovePoint => transform.DOMove(movePosition, moveSpeed);
    public Tween ToOriginPoint => transform.DOMove(originPosition, moveSpeed); 
    
    // UI
    public Tween ToMovePointUI => GetComponent<RectTransform>().DOAnchorPos(movePosition, moveSpeed);
    public Tween ToOriginPointUI => GetComponent<RectTransform>().DOAnchorPos(originPosition, moveSpeed);
}
