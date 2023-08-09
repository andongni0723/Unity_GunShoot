using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class MenuButtonManager : MonoBehaviour
{
    [Header("Data")]
    public CharacterDetails_SO currentCharacterDetails;

    [Header("Components")]
    public GameObject characterShowSprite;
    public GameObject characterName;
    public GameObject leftUIGameObject;
    public GameObject rightUIGameObject;
    public GameObject backButtonGameObject;
    
    // Move Point
    [Space(20)]
    public Transform leftUIOriginPoint;
    public Transform leftUIMovePoint;
    public Transform rightUIOriginPoint;
    public Transform rightUIMovePoint;
    public Transform backButtonOriginPoint;
    public Transform backButtonMovePoint;
    public Transform characterShowSpriteMoveLeftPoint;
    public Transform characterShowSpriteMoveRightPoint;

    [Header("Setting")]
    public float leftUIMoveSpeed = 2;
    public float rightUIMoveSpeed = 2;
    
    //[Header("Var")]
    public Action BackButtonAction;

    #region Button Methods

    public void PlayButton()
    {
        
    }

    public void CharacterButton()
    {
        Sequence sequence = DOTween.Sequence();
        
        // Right UI move to Screen out
        sequence.Append(rightUIGameObject.transform.DOMove(rightUIMovePoint.position, rightUIMoveSpeed)); 
        
        // Left UI move to Screen out
        sequence.Join(leftUIGameObject.transform.DOMove(leftUIMovePoint.position, leftUIMoveSpeed)); 
        
        // CharacterShowSprite and Name move to Right
        sequence.Join(characterShowSprite.transform.DOMove(characterShowSpriteMoveRightPoint.position, leftUIMoveSpeed));
        sequence.Join(characterName.transform.DOMove(characterShowSpriteMoveRightPoint.position, leftUIMoveSpeed));
        
        // Setting Button move to Screen in
        sequence.Join(backButtonGameObject.transform.DOMove(backButtonMovePoint.position, leftUIMoveSpeed));
        
        // Setting Back Button Action
        BackButtonAction = () =>
        {
            Sequence backSequence = DOTween.Sequence();
            
            // Right UI move to Screen in
            backSequence.Append(rightUIGameObject.transform.DOMove(rightUIOriginPoint.position, rightUIMoveSpeed));
            
            // Left UI move to Screen in
            backSequence.Join(leftUIGameObject.transform.DOMove(leftUIOriginPoint.position, leftUIMoveSpeed));
            
            // CharacterShowSprite and Name move to Left
            backSequence.Join(characterShowSprite.transform.DOMove(characterShowSpriteMoveLeftPoint.position, leftUIMoveSpeed));
            backSequence.Join(characterName.transform.DOMove(characterShowSpriteMoveLeftPoint.position, leftUIMoveSpeed));
            
            // Setting Button move to Screen out
            backSequence.Join(backButtonGameObject.transform.DOMove(backButtonOriginPoint.position, leftUIMoveSpeed));
        };
    }

    public void SettingButton()
    {
        
    }
    
    public void BackButton()
    {
        BackButtonAction?.Invoke();
        BackButtonAction = null;
    }

    #endregion
}
