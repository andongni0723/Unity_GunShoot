using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyRecoveryItemButton : BuyButton
{
    protected override void Start()
    {
        // Add Listener
        button.onClick.AddListener(OnBuy);;
    }
}
