using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<CharacterShowDetails> allCharacterDetailsList;

    private void Start()
    {
        // Call Character Panel Update Details
        EventHandler.CallUpdateCharacterList(allCharacterDetailsList);
    }
}
