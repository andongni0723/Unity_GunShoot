using System;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static event Action PlayerDead;
    public static void CallPlayerDead()
    {
        PlayerDead?.Invoke();
    }

    public static event Action<WeaponDetails_SO, WeaponDetails_SO, WeaponDetails_SO> ReadPlayDetail;

    public static void CallReadPlayDetail(WeaponDetails_SO mainWeapon, WeaponDetails_SO secondWeapon, WeaponDetails_SO currentWeapon)
    {
        ReadPlayDetail?.Invoke(mainWeapon, secondWeapon, currentWeapon);
    }

    public static event Action<WeaponDetails_SO> UpdatePlayerDetails;

    public static void CallUpdatePlayerDetails(WeaponDetails_SO currentWeapon)
    {
        UpdatePlayerDetails?.Invoke(currentWeapon);
    }

    public static event Action<WeaponDetails_SO> ChangeWeapon;

    public static void CallChangeWeapon(WeaponDetails_SO data)
    {
        ChangeWeapon?.Invoke(data);
    }

    public static event Action WeaponReload;

    public static void CallWeaponReload()
    {
        WeaponReload?.Invoke();
    }

    public static event Action WeaponReloadEnd;

    public static void CallWeaponReloadEnd()
    {
        WeaponReloadEnd?.Invoke();
    }

    public static event Action<float> ChangeCameraSight;

    public static void CallChangeCameraSight(float target)
    {
        ChangeCameraSight?.Invoke(target);
    }
}
