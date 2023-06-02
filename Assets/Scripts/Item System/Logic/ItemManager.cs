using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject skillItem;

    public void UseSkillItem(Vector2 startForce, Vector3 itemPosition, Quaternion rotation)
    {
        GameObject item = Instantiate(skillItem, itemPosition, rotation);
        item.GetComponent<ThrowItem>().Throw(startForce);
    }

}
