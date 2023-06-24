using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerObject;
    public GameObject playerHeadObject;

    [Header("Data")] 
    public GamePlatform gamePlatform;
    public CharacterDetails_SO playerDetail;
    public int currentPlayerMoney = 250;
    public List<WeaponBulletData> PlayerWeaponBulletDataList = new List<WeaponBulletData>();

    protected override void Awake()
    {
        base.Awake();
        CheckGamePlatform();
        Application.targetFrameRate = 300;
        StartCoroutine(WaitForGetPlayerDetails());
    }

    private void CheckGamePlatform()
    {
        
#if UNITY_ANDROID && UNITY_IOS
        gamePlatform = GamePlatform.Mobile;
        
#elif UNITY_STANDALONE
        gamePlatform = GamePlatform.PC;
#endif
    }

    IEnumerator WaitForGetPlayerDetails()
    {
        yield return new WaitUntil(() => playerDetail != default);
        Debug.Log("Load Player");
        EventHandler.CallLoadPlayer(playerDetail);
    }

    public WeaponBulletData LoadPlayerWeaponBulletData(WeaponDetails_SO loadData)
    {
        foreach (var data in PlayerWeaponBulletDataList)
        {
            if (data.weaponDetails == loadData)
            {
                return data;
            }
        }
        
        // Not Found
        Debug.LogWarning($"Not Found WeaponBulletData: '{loadData}'");
        Debug.Log($"Not Found WeaponBulletData: '{loadData}'");
        
        WeaponBulletData newData = new WeaponBulletData(loadData, loadData.clipBulletCount, loadData.bagBulletCount);
        PlayerWeaponBulletDataList.Add(newData);
        return newData;
    }

    public void SavePlayerWeaponBulletData(WeaponBulletData updateData)
    {
        foreach (var data in PlayerWeaponBulletDataList)
        {
            if (data.weaponDetails == updateData.weaponDetails)
            {
                data.currentBulletCount = updateData.currentBulletCount;
                data.currentBagBulletCount = updateData.currentBagBulletCount;
                return;
            }
        }
        
        // Not Found
        PlayerWeaponBulletDataList.Add(updateData);
    }
}
