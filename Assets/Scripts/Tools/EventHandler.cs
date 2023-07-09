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

    public static event Action<GameObject> InteractiveItem;

    public static void CallInteractiveItem(GameObject targetItem)
    {
        InteractiveItem?.Invoke(targetItem);
    }

    public static event Action OnCancelUI;

    public static void CallOnCancelUI()
    {
        OnCancelUI?.Invoke();
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

    public static event Action OpenStorePanel;

    public static void CallOpenStorePanel()
    {
        OpenStorePanel?.Invoke();
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

    public static event Action<BuyItemDetails> BuyItem;

    public static void CallBuyItem(BuyItemDetails data)
    {
        BuyItem?.Invoke(data);
    }

    public static event Action<BuyItemDetails> CorrectBuyItem;

    public static void CallCorrectBuyItem(BuyItemDetails data)
    {
        CorrectBuyItem?.Invoke(data);
    }

    public static event Action CancelBuyItem;

    public static void CallCancelBuyItem()
    {
        CancelBuyItem?.Invoke();
    }

    public static event Action<BuyItemDetails> BuyItemSuccessful;

    public static void CallBuyItemSuccessful(BuyItemDetails data)
    {
        BuyItemSuccessful?.Invoke(data);
    }

    #endregion

    #region Mission System

    public static event Action<MissionDetails_SO> NewMission;

    public static void CallNewMission(MissionDetails_SO data)
    {
        NewMission?.Invoke(data);
    }

    #endregion
}
