using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    private Button button => GetComponent<Button>();

    private void Awake()
    {
        button.onClick.AddListener(AudioManager.Instance.ButtonClickAudio);
    }
}
