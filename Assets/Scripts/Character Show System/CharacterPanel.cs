using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    public GameObject CharacterToggleObject;
    public GameObject CharacterToggleParent;

    private bool _isFirstToggle = true;

    #region Event

    private void OnEnable()
    {
        EventHandler.UpdateCharacterList += OnUpdateCharacterList; // Spawn Character Toggle with List which event send
    }

    private void OnDisable()
    {
        EventHandler.UpdateCharacterList -= OnUpdateCharacterList;
    }

    private void OnUpdateCharacterList(List<CharacterShowDetails> dataList)
    {
        // Update Character List
        foreach (var data in dataList)
        {
            CharacterToggle newCharacterToggle = Instantiate(CharacterToggleObject, CharacterToggleParent.transform).GetComponent<CharacterToggle>();
            newCharacterToggle.characterShowDetails = data;
            
            // If first toggle, set isOn to true, and set characterShow to it
            if(_isFirstToggle)
            {
                newCharacterToggle.GetComponent<Toggle>().isOn = true;
                _isFirstToggle = false;
            }
        }
    }

    #endregion 
}
