using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterToggle : MonoBehaviour
{
    [Header("Data")]
    public CharacterShowDetails characterShowDetails;


    [Header("Component")]
    public TextMeshProUGUI characterNameText;
    private Toggle toggle => GetComponent<Toggle>();

    [Header("Setting")]
    public Color normalColor;
    public Color selectedColor;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => characterShowDetails != default);
        
        // Set Data
        characterNameText.text = characterShowDetails.characterDetails.characterName;
        toggle.group = transform.parent.GetComponent<ToggleGroup>();
    }

    public void OnValueChange()
    {
        // Check toggle isOn, if true, change color to selectedColor
        toggle.targetGraphic.color = toggle.isOn ? selectedColor : normalColor;

        // On Click
        if (toggle.isOn)
        {
            EventHandler.CallClickCharacterToggle(characterShowDetails);
        }
    }
}
