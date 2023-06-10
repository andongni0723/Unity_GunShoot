using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject skillItem;
    public TextMeshProUGUI skillCountText;
    public int currentSkillCount;

    private void Start()
    {
        skillCountText.text = currentSkillCount.ToString();
    }

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
}
