using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Game Data/Weapon Detail", fileName = "Details_SO")]
public class WeaponDetails_SO : ScriptableObject
{
    
    [HorizontalGroup("Basic")]
    [VerticalGroup("Basic/Image")]
    [PreviewField(100, ObjectFieldAlignment.Left), HideLabel]
    public Sprite weaponSprite;
  
    [FormerlySerializedAs("weaponNme")] [FormerlySerializedAs("name")] [VerticalGroup("Basic/Details")]
    public string weaponName;
    
    [VerticalGroup("Basic/Details"), MinValue(0)]
    public int clipBulletCount;
    
    [VerticalGroup("Basic/Details"), MinValue(0)]
    public int bagBulletCount;
    [VerticalGroup("Basic/Details"), MinValue(0)]
    public float weaponReloadTime;

    public GunShootMode gunShootMode;
    
    [MinMaxSlider(0, 45, true), SerializeField]
    private Vector2 shootAngle;
    public AudioClip fireAudio;
    public float shootCooldown;
    public int damage = 1;
    
    [Header("Camera Setting")]
    public float cameraSight = 5.5f;
    public float lightOutSight = 7.78f;
    public float fireCameraShake = 10;
    
    public float minShootAngle => shootAngle.x;

    public float maxShootAngle => shootAngle.y;
}
