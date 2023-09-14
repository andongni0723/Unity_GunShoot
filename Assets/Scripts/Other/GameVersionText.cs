using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameVersionText : MonoBehaviour
{
    private TextMeshProUGUI text => GetComponent<TextMeshProUGUI>();
    private void Awake()
    {
        text.text = $"v{Application.version}";
    }
}
