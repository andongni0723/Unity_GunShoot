using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject skillItem;

    public void UseSkillItem(Vector2 startForce, Vector3 itemPosition)
    {
        GameObject item = Instantiate(skillItem, itemPosition, Quaternion.identity);
        item.GetComponent<ThrowItem>().Throw(startForce);
    }

}
