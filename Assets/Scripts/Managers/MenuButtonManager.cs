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
    public ButtonMoveDetails characterShowScript;
    public ButtonMoveDetails characterNameScript;
    public ButtonMoveDetails leftUIGameScript;
    public ButtonMoveDetails rightUIGameScript;
    public ButtonMoveDetails backButtonScript;
    public ButtonMoveDetails characterPanelScript;
    public ButtonMoveDetails characterDetailsPanelScript;

    //[Header("Var")]
    public Action BackButtonAction;

    #region Event

    private void OnEnable()
    {
        EventHandler.ClickCharacterToggle += OnClickCharacterToggle; // Set current character details
    }

    private void OnDisable()
    {
        EventHandler.ClickCharacterToggle -= OnClickCharacterToggle;
    }

    private void OnClickCharacterToggle(CharacterShowDetails data)
    {
        currentCharacterDetails = data.characterDetails;
    }

    #endregion 
    

    #region Button Methods

    public void PlayButton()
    {
        // Save Character Details and Load Game Scene
        MainSceneDataManager.Instance.playerChooseCharacterDetails = currentCharacterDetails;
        MainSceneDataManager.Instance.LoadGameScene();
    }

    public void CharacterButton()
    {
        Sequence sequence = DOTween.Sequence();
        
        // Right UI move to Screen out
        sequence.Append(rightUIGameScript.ToMovePointUI);
        
        // Left UI move to Screen out
        sequence.Join(leftUIGameScript.ToMovePointUI);

        
        // Setting Button move to Screen in
        sequence.Join(backButtonScript.ToMovePointUI);
        
        // CharacterShowSprite and Name move to Right
        sequence.Join(characterShowScript.ToMovePoint);
        sequence.Join(characterNameScript.ToMovePointUI);
        
        // Character Panel move to Screen in
        sequence.Join(characterPanelScript.ToMovePointUI);
        
        // Character Details Panel move to Screen in
        sequence.Join(characterDetailsPanelScript.ToMovePointUI);

        // Setting Back Button Action
        BackButtonAction = () =>
        {
            Sequence backSequence = DOTween.Sequence();
            
            // Right UI move to Screen in
            backSequence.Append(rightUIGameScript.ToOriginPointUI);
            
            // Left UI move to Screen in
            backSequence.Join(leftUIGameScript.ToOriginPointUI);
            
            // CharacterShowSprite and Name move to Left
            backSequence.Join(characterShowScript.ToOriginPoint);
            backSequence.Join(characterNameScript.ToOriginPointUI);
            
            // Setting Button move to Screen out
            backSequence.Join(backButtonScript.ToOriginPointUI);
            
            // Character Panel move to Screen out
            backSequence.Join(characterPanelScript.ToOriginPointUI);
            
            // Character Details Panel move to Screen out
            backSequence.Join(characterDetailsPanelScript.ToOriginPointUI);
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
