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
    public bool isMapPrepare; // ItemRandomSpawnManager will spawn console and enemys 

    [Space(20)]
    // The details var will be call by the other scripts
    public int maxEnemyCount;   // ItemRandomSpawnManager 
    public int killEnemyCount;  // EnemyHealth
    public int spendHpCount;    // PlayerHealth
    public int spendMoneyCount; // StorePanel
    
    [Header("Setting")] 
    public bool isTest;

    protected override void Awake()
    {
        base.Awake();
        if(!isTest) 
            CheckGamePlatform();
        
        playerDetail = MainSceneDataManager.Instance.playerChooseCharacterDetails;
        
        StartCoroutine(WaitForGetPlayerDetailsAndMapPrepare());
    }

    #region Event

    private void OnEnable()
    {
        EventHandler.GameWin += OnGameWin;
    }

    private void OnDisable()
    {
        EventHandler.GameWin -= OnGameWin;

    }

    private void OnGameWin()
    {
    }

    #endregion 
    
    private void CheckGamePlatform()
    {
        
#if UNITY_ANDROID && UNITY_IOS
        gamePlatform = GamePlatform.Mobile;
        
#elif UNITY_STANDALONE
        gamePlatform = GamePlatform.PC;
#endif
    }

    IEnumerator WaitForGetPlayerDetailsAndMapPrepare()
    {
        yield return new WaitUntil(() => playerDetail != default && isMapPrepare);
        Debug.Log("Load Player");
        EventHandler.CallLoadPlayer(playerDetail);
    }

    #region Tools

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

    public void KillEnemyAddMoney()
    {
        currentPlayerMoney += 20;
    }
    
    public void AgainButtonEvent()
    {
        MainSceneDataManager.Instance.LoadStartScene();
    }

    #endregion
    
}
