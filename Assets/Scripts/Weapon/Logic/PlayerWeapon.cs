using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class PlayerWeapon : BaseWeapon
{
    [Header("Component")] 
    public Light2D playerLight;
    public Light2D gunLight;
    public PolygonCollider2D lightCollider;
    
    [Header("UI")] 
    public Image currentWeaponImage;
    public TextMeshProUGUI currentBulletText;
    public TextMeshProUGUI currentBagBulletText;

    //Health
    public Slider shootCooldownBar;
    public Slider reloadBulletBar;

    protected override void Awake()
    {
        // Init late to load player details done
        
        // Set BaseWeapon action
        SetSaveBulletDataAction(delegate { GameManager.Instance.SavePlayerWeaponBulletData(data); });
        SetLoadBulletDataAction(delegate { GameManager.Instance.LoadPlayerWeaponBulletData(currentWeapon); });
        SetWeaponReloadAction(EventHandler.CallWeaponReload);
        SetWeaponReloadEndAction(EventHandler.CallWeaponReloadEnd);
        
    }

    #region Event

    private void OnEnable()
    {
        EventHandler.LoadPlayerEnd += OnLoadPlayerEnd; // Init
        EventHandler.ChangeWeapon += OnChangeWeapon; // Change weapon 
        EventHandler.BuyItemSuccessful += OnBuyItemSuccessful; // Check player has buy main weapon
    }

    private void OnDisable()
    {
        EventHandler.LoadPlayerEnd -= OnLoadPlayerEnd;
        EventHandler.ChangeWeapon -= OnChangeWeapon;
        EventHandler.BuyItemSuccessful -= OnBuyItemSuccessful;
    }
    
    private void OnLoadPlayerEnd()
    {
        Debug.Log("INIT");
        currentWeapon = weaponList[0];

        // New bullet data from currentWeapon
        data =
            new WeaponBulletData(currentWeapon, currentWeapon.clipBulletCount, currentWeapon.bagBulletCount);
        
        //EventHandler.CallBuyItemSuccessful(tb);
        //OnChangeWeapon(currentWeapon);
    }

    private void OnChangeWeapon(WeaponDetails_SO _data)
    {
        CancelReloadBullet();
        
        currentWeapon = _data;
        data = GameManager.Instance.LoadPlayerWeaponBulletData(currentWeapon);
        
        // Reset the gun light with the new weapon details
        playerLight.pointLightOuterRadius = data.weaponDetails.lightOutSight;
        playerLight.pointLightInnerRadius = data.weaponDetails.lightOutSight - 0.64f;
        gunLight.pointLightOuterRadius = data.weaponDetails.lightOutSight;
        gunLight.pointLightInnerRadius = data.weaponDetails.lightOutSight - 0.64f;
        
        LightSightToColliderSize(data.weaponDetails.lightOutSight);
        AudioManager.Instance.PlayItemAudio(AudioManager.Instance.changeWeaponAudio);
        EventHandler.CallChangeCameraSight(data.weaponDetails.cameraSight);
        UpdateUIDetails();
    }

    private void OnBuyItemSuccessful(BuyItemDetails _data)
    {
        switch (_data.itemType)
        {
            case ItemType.Weapon:
                weaponList[0] = _data.buyWeaponDetails; // Set first weapon (None) to main weapon of buy
                OnChangeWeapon(weaponList[0]); // Change Weapon to main weapon
                break;
        } 
    }

    #endregion

    protected override void Update()
    {
        // Timer
        if (!isReloadTimerEnd)
        {
            reloadTimer += Time.deltaTime;
    
            // Value 1 to 0 with reload time
            reloadBulletBar.gameObject.SetActive(true);
            reloadBulletBar.value = (data.weaponDetails.weaponReloadTime - reloadTimer) / data.weaponDetails.weaponReloadTime;
            
            if (reloadTimer >= endTime)
            {
                isReloadTimerEnd = true;
                reloadBulletBar.gameObject.SetActive(false);
            }
        }
        
        if (!isShootCooldownTimerEnd)
        {
            shootCooldownTimer += Time.deltaTime;
            
            // Value 1 to 0 with shoot cooldown time
            shootCooldownBar.gameObject.SetActive(true);
            shootCooldownBar.value = (data.weaponDetails.shootCooldown - shootCooldownTimer) / data.weaponDetails.shootCooldown;

            if (shootCooldownTimer >= endTime)
            {
                isShootCooldownTimerEnd = true;
                shootCooldownBar.gameObject.SetActive(false);
            }
        }
        
        UpdateUIDetails(); 
    }

    private void UpdateUIDetails()
    {
        if (currentWeapon != null)
        {
            currentWeaponImage.sprite = data.weaponDetails.weaponSprite;
            currentWeaponImage.SetNativeSize(); 
            currentBulletText.text = data.currentBulletCount.ToString();
            currentBagBulletText.text = $"/{data.currentBagBulletCount.ToString()}";  
        }
    }

    private void LightSightToColliderSize(float lightSight)
    {
        Vector2[] newPoint = lightCollider.points;
        newPoint[1] = new Vector2(0.43f * lightSight - 0.1754f, 0.93f * lightSight - 0.1054f);    // right out path
        newPoint[3] = new Vector2(-(0.43f * lightSight - 0.1754f), 0.93f * lightSight - 0.1054f); // left out path
        newPoint[2] = new Vector2(0, lightSight - 0.03f);                                         // center  path
        
        lightCollider.SetPath(0, newPoint); 
    }

    /// <summary>
    /// The angle of shoot will be change by character speed
    /// </summary>
    /// <param name="speed"></param>
    public override void SpeedToChangeShootAngle(float speed)
    {
        if (currentWeapon != null)
        {
            currentAngle = currentWeapon.minShootAngle +
                           speed * (currentWeapon.maxShootAngle - currentWeapon.minShootAngle);
        
            weaponLight.pointLightInnerAngle = currentAngle;
            weaponLight.pointLightOuterAngle = currentAngle;
        }
    }

    public override IEnumerator ReloadBullet()
    {
        if (data.currentBagBulletCount > 0)
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.reloadWeaponAudio);
        }
        return base.ReloadBullet();
    }

    protected override void CancelReloadBullet()
    {
        reloadBulletBar.value = 0; 
        base.CancelReloadBullet();
    }
}
