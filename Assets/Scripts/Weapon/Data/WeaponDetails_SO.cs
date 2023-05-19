using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Game Data", fileName = "Weapon Detail")]
public class WeaponDetails_SO : ScriptableObject
{
    
    [HorizontalGroup("Basic")]
    [VerticalGroup("Basic/Image")]
    [PreviewField(100, ObjectFieldAlignment.Left), HideLabel]
    public Sprite weaponSprite;
  
    [VerticalGroup("Basic/Details")]
    public string name;
    
    [VerticalGroup("Basic/Details"), MinValue(0)]
    public int clipBulletCount;
    
    [VerticalGroup("Basic/Details"), MinValue(0)]
    public int bagBulletCount;
    [VerticalGroup("Basic/Details"), MinValue(0)]
    public float weaponReloadTime;

    [MinMaxSlider(0, 45, true), SerializeField]
    private Vector2 shootAngle;

    public AudioClip fireAudio;
    
    public float minShootAngle => shootAngle.x;

    public float maxShootAngle => shootAngle.y;
}
