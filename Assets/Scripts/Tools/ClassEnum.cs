// Enum

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