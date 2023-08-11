using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterShowController : MonoBehaviour
{
    [Header("Data")]
    public GameObject currentShowCharacter;
    
    [Header("Component")]
    public TextMeshProUGUI characterNameText;


    #region Event

    private void OnEnable()
    {
        EventHandler.ClickCharacterToggle += OnClickCharacterToggle; // Execute open character show sprite
    }

    private void OnDisable()
    {
        EventHandler.ClickCharacterToggle -= OnClickCharacterToggle;
    }

    private void OnClickCharacterToggle(CharacterShowDetails data)
    {
        currentShowCharacter.SetActive(false);
        
        // Update New
        data.characterShowObject.SetActive(true);
        characterNameText.text = data.characterDetails.characterName;
        
        currentShowCharacter = data.characterShowObject;
    }

    #endregion 
    
}
