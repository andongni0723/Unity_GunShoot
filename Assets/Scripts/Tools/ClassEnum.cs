// Enum

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GamePlatform
{
    PC, Mobile
}

public enum EnemyState
{
    Static, Chase, Attack, Back
}

public enum CharacterHeadState
{
    Left, Center, Right
}

public enum ItemType
{
    Weapon, Skill, Healthy , Buff
}

public enum GunShootMode
{
    semi_auto, fully_auto
}

public enum MissionType
{
    MainMission, OtherMission
}

public enum MissionContentType
{
    GotoItemPoint, CountSomeThing
}

// Class
[System.Serializable]
public class WeaponBulletData
{
    public WeaponDetails_SO weaponDetails;
    public int currentBulletCount;
    public int currentBagBulletCount;

    public WeaponBulletData(WeaponDetails_SO weaponDetails, int currentBulletCount, int currentBagBulletCount)
    {
        this.weaponDetails = weaponDetails;
        this.currentBulletCount = currentBulletCount;
        this.currentBagBulletCount = currentBagBulletCount;
    }
}

[System.Serializable]
public class BuyItemDetails
{
    public ItemType itemType;
    public string itemName;
    public int itemPrice;
    public WeaponDetails_SO buyWeaponDetails;
    public int buySkillObjectCount;
    public int buyHealthObjectCount;
}

[System.Serializable]
public class RandomSpawnItemDetails
{
    public List<GameObject> SpawnPointsList = new List<GameObject>();
    public GameObject spawnItem;
    public int spawnCount = 1;
}

[System.Serializable]
public class CharacterShowDetails
{
    public int characterID;
    public GameObject characterShowObject;
    public CharacterDetails_SO characterDetails;
}

[System.Serializable]
public class CharacterDetailsShowUI
{
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI characterHealthText;
    public TextMeshProUGUI characterDefenseText;
    public Slider characterDefenseSlider;
    public TextMeshProUGUI characterRecoveryText;
}

[System.Serializable]
public class WeaponDetailsShowUI
{
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI weaponShootModeText;
    public Image weaponImage;
    public TextMeshProUGUI clipBulletCountText;
    public TextMeshProUGUI bagBulletCountText;
    public TextMeshProUGUI weaponDamageText;
    public TextMeshProUGUI shootCooldownText;
    public TextMeshProUGUI reloadTimeText;
    public TextMeshProUGUI shootRangeText;
    
    public TextMeshProUGUI cameraSightText;
    public TextMeshProUGUI lightOutSightText;
    public TextMeshProUGUI fireCameraShakeText;
}

[System.Serializable]
public class CharacterSkillDetailsShowUI
{
    public TextMeshProUGUI skillNameText;
    public Image skillImage;
    public TextMeshProUGUI skillDescriptionText;
}