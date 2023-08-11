using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Game Data/Character Details", fileName = "CharacterDetails_SO")]
public class CharacterDetails_SO : ScriptableObject
{

    [HorizontalGroup("Basic")]
    [VerticalGroup("Basic/Image")]
    [PreviewField(100, ObjectFieldAlignment.Left), HideLabel]
    [SerializeField]
    private Sprite mainWeaponSprite;

    [VerticalGroup("Basic/Image")] [PreviewField(100, ObjectFieldAlignment.Left), HideLabel] [SerializeField]
    private Sprite secondWeaponSprite;

    [VerticalGroup("Basic/Image")] [PreviewField(100, ObjectFieldAlignment.Left), HideLabel] [SerializeField]
    public Sprite skillItemSprite;

    [VerticalGroup("Basic/Details")] public string characterName;

    [VerticalGroup("Basic/Details"), MinValue(1)]
    public int startHealth = 100;

    [VerticalGroup("Basic/Details"), MinValue(0), MaxValue(100)]
    public int startDefense = 50;

    [VerticalGroup("Basic/Details")] public WeaponDetails_SO mainWeaponDetails;
    [VerticalGroup("Basic/Details")] public WeaponDetails_SO secondWeaponDetails;

    [VerticalGroup("Basic/Details")] public GameObject skillItemDetails;

    [VerticalGroup("Basic/Details")] public string skillItemName;

    public List<WeaponDetails_SO> startWeaponList = new List<WeaponDetails_SO>();

    public int recoveryHealth = 30;

    [Header("Skill")]
    [TextArea]public string skillDescription;
}