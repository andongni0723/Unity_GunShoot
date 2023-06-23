using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    //[Header("Component")] 
    private PlayerHealth playerHealth => GetComponent<PlayerHealth>();
    
    [Header("Skill Item")]
    public GameObject skillItem;
    public Image skillItemIconImage;
    public TextMeshProUGUI skillCountText;
    public int currentSkillCount;
    
    [Header("Recovery Item")]
    public TextMeshProUGUI recoveryCountText;
    public int currentRecoveryCount;
    
    private void Start()
    {
        skillCountText.text = currentSkillCount.ToString();
        recoveryCountText.text = currentRecoveryCount.ToString();
    }

    #region Event

    private void OnEnable()
    {
        EventHandler.BuyItemSuccessful += OnBuyItemSuccessful;
    }

    private void OnDisable()
    {
        EventHandler.BuyItemSuccessful -= OnBuyItemSuccessful;
    }

    private void OnBuyItemSuccessful(BuyItemDetails data)
    {
        switch (data.itemType)
        {
            case ItemType.Skill:
                currentSkillCount += data.buySkillObjectCount;
                skillCountText.text = currentSkillCount.ToString();
                break;
            
            case ItemType.Healthy:
                currentRecoveryCount += data.buyHealthObjectCount;
                recoveryCountText.text = currentRecoveryCount.ToString();
                break;
        }
    }

    #endregion 

    public void UseSkillItem(Vector2 startForce, Vector3 itemPosition, Quaternion rotation)
    {
        if (currentSkillCount > 0)
        {
            currentSkillCount--;
            skillCountText.text = currentSkillCount.ToString();
            GameObject item = Instantiate(skillItem, itemPosition, rotation);
            item.GetComponent<ThrowItem>().Throw(startForce);
        }
    }

    public void UseRecoveryItem(int increaseValue)
    {
        if (currentRecoveryCount > 0)
        {
            currentRecoveryCount--;
            recoveryCountText.text = currentRecoveryCount.ToString();
            playerHealth.Recovery(increaseValue);
        }
    }
}
