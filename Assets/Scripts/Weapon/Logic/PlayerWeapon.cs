using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerWeapon : BaseWeapon
{
    [Header("UI")] 
    public TextMeshProUGUI currentBulletText;
    public TextMeshProUGUI currentBagBulletText;
    
    //Health
    public Slider healthBar;

    protected override void Update()
    {
        // Timer
        if (!isTimerEnd)
        {
            timer += Time.deltaTime;
    
            // / value 1 to 0 with reload time
            healthBar.gameObject.SetActive(true);
            healthBar.value = currentWeapon.weaponReloadTime - timer / currentWeapon.weaponReloadTime;
            
            if (timer >= endTime)
            {
                isTimerEnd = true;
                healthBar.gameObject.SetActive(false);
            }
        }
        
        currentBulletText.text = currentBulletCount.ToString();
        currentBagBulletText.text = $"/{currentBagBulletCount.ToString()}";
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
