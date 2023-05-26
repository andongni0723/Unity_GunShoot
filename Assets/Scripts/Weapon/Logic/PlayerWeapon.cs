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
    public TextMeshProUGUI currentBulletText;
    public TextMeshProUGUI currentBagBulletText;
    
    //Health
    public Slider shootCooldownBar;
    public Slider reloadBulletBar;

    protected override void Update()
    {
        // Timer
        if (!isReloadTimerEnd)
        {
            reloadTimer += Time.deltaTime;
    
            // Value 1 to 0 with reload time
            reloadBulletBar.gameObject.SetActive(true);
            reloadBulletBar.value = currentWeapon.weaponReloadTime - reloadTimer / currentWeapon.weaponReloadTime;
            
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
            reloadBulletBar.value = currentWeapon.shootCooldown - shootCooldownTimer / currentWeapon.shootCooldown;

            if (shootCooldownTimer >= endTime)
            {
                isShootCooldownTimerEnd = true;
                shootCooldownBar.gameObject.SetActive(false);
            }
        }
        
        currentBulletText.text = currentBulletCount.ToString();
        currentBagBulletText.text = $"/{currentBagBulletCount.ToString()}";
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
        if (currentBagBulletCount > 0)
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.reloadWeaponAudio);
        }
        return base.ReloadBullet();
    }
}
