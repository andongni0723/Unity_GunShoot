using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public class PlayerWeapon : BaseWeapon
{
    [Header("UI")] 
    public Image currentWeaponImage;
    public TextMeshProUGUI currentBulletText;
    public TextMeshProUGUI currentBagBulletText;
    
    //Health
    public Slider shootCooldownBar;
    public Slider reloadBulletBar;

    protected override void Awake()
    {
        base.Awake();
        
        SetSaveBulletDataAction(delegate { GameManager.Instance.SavePlayerWeaponBulletData(data); });
        SetLoadBulletDataAction(delegate { GameManager.Instance.LoadPlayerWeaponBulletData(currentWeapon); });
    }

    #region Event

    private void OnEnable()
    {
        EventHandler.ChangeWeapon += OnChangeWeapon;
    }

    private void OnDisable()
    {
        EventHandler.ChangeWeapon -= OnChangeWeapon;
    }

    private void OnChangeWeapon(WeaponDetails_SO _data)
    {
        currentWeapon = _data;
        
        data = GameManager.Instance.LoadPlayerWeaponBulletData(currentWeapon);
        UpdateUIDetails();
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
            reloadBulletBar.value = data.weaponDetails.shootCooldown - shootCooldownTimer / data.weaponDetails.shootCooldown;

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
        currentWeaponImage.sprite = data.weaponDetails.weaponSprite;
        currentWeaponImage.SetNativeSize(); 
        currentBulletText.text = data.currentBulletCount.ToString();
        currentBagBulletText.text = $"/{data.currentBagBulletCount.ToString()}"; 
    }

    /// <summary>
    /// The angle of shoot will be change by character speed
    /// </summary>
    /// <param name="speed"></param>
    public override void SpeedToChangeShootAngle(float speed)
    {
        currentAngle = currentWeapon.minShootAngle +
                       speed * (currentWeapon.maxShootAngle - currentWeapon.minShootAngle);
        
        weaponLight.pointLightInnerAngle = currentAngle;
        weaponLight.pointLightOuterAngle = currentAngle;
    }

    public override IEnumerator ReloadBullet()
    {
        if (data.currentBagBulletCount > 0)
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.reloadWeaponAudio);
        }
        return base.ReloadBullet();
    }
}
