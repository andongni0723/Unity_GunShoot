// Enum

using UnityEngine.Serialization;

public enum EnemyState
{
    Static, Chase, Attack, Back
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
