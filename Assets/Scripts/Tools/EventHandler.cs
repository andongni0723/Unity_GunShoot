using System;
using JetBrains.Annotations;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static event Action<CharacterDetails_SO> LoadPlayer;

    public static void CallLoadPlayer(CharacterDetails_SO data)
    {
        LoadPlayer?.Invoke(data);
    }

    public static event Action LoadPlayerEnd;

    public static void CallLoadPlayerEnd()
    {
        LoadPlayerEnd?.Invoke();
    }
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

    public static event Action StorePanelLoadingDone;

    public static void CallStorePanelLoadingDone()
    {
        StorePanelLoadingDone?.Invoke();
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

    #region Store

    public static event Action<ItemType, WeaponDetails_SO, int> BuyItem;

    public static void CallBuyItem(ItemType itemType, WeaponDetails_SO data, int itemPrice)
    {
        BuyItem?.Invoke(itemType, data, itemPrice);
    }

    public static event Action<WeaponDetails_SO, int> CorrectBuyItem;

    public static void CallCorrectBuyItem(WeaponDetails_SO data, int itemPrice)
    {
        CorrectBuyItem?.Invoke(data, itemPrice);
    }

    public static event Action CancelBuyItem;

    public static void CallCancelBuyItem()
    {
        CancelBuyItem?.Invoke();
    }

    public static event Action BuyItemSuccessful;

    public static void CallBuyItemSuccessful()
    {
        BuyItemSuccessful?.Invoke();
    }

    #endregion
}
