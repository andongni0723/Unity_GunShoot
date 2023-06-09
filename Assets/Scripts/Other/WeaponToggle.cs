using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponToggle : MonoBehaviour
{
    private Toggle toggle => GetComponent<Toggle>();
    public Image weaponImage;
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponCurrentBulletsText;
    public TextMeshProUGUI weaponCurrentBugBulletsText;
    public GameObject openOutLineImage;
    
    public WeaponDetails_SO weaponDetails;
    [SerializeField] private WeaponBulletData data;

    private void Awake()
    {
        toggle.onValueChanged.AddListener(delegate { ToggleValueChange(); });
        openOutLineImage.SetActive(toggle.isOn);
    }

    #region Event

    private void OnEnable()
    {
        EventHandler.UpdatePlayerDetails += OnUpdatePlayerDetails; // update toggle and children UI
        EventHandler.WeaponReload += OnWeaponReload; // Set toggle can't use
        EventHandler.WeaponReloadEnd += OnWeaponReloadEnd; // Set toggle can use
    }

    private void OnDisable()
    {
        EventHandler.UpdatePlayerDetails -= OnUpdatePlayerDetails;
        EventHandler.WeaponReload -= OnWeaponReload;
        EventHandler.WeaponReloadEnd -= OnWeaponReloadEnd;
    }

    private void OnUpdatePlayerDetails(WeaponDetails_SO playerCurrentWeapon)
    {
        data = GameManager.Instance.LoadPlayerWeaponBulletData(weaponDetails);
        
        // Update UI 
        weaponImage.sprite = data.weaponDetails.weaponSprite;
        weaponImage.SetNativeSize();
        weaponName.text = data.weaponDetails.weaponName;
        weaponCurrentBulletsText.text = data.currentBulletCount.ToString();
        weaponCurrentBugBulletsText.text = $"/{data.currentBagBulletCount.ToString()}";

        toggle.isOn = playerCurrentWeapon == weaponDetails;
        openOutLineImage.SetActive(toggle.isOn);
    }

    private void OnWeaponReload()
    {
        toggle.enabled = false;
    }

    private void OnWeaponReloadEnd()
    {
        toggle.enabled = true;
    }

    #endregion 

    private void ToggleValueChange()
    {
        openOutLineImage.SetActive(toggle.isOn);
        EventHandler.CallChangeWeapon(weaponDetails); 
    }
}
