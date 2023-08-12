using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CharacterDetailsPanel : MonoBehaviour
{
    [Header("Data")]
    public CharacterDetails_SO characterDetails;
    
    [Header("Component")]
    public CharacterDetailsShowUI characterDetailsShowUI;
    public WeaponDetailsShowUI mainWeaponDetailsShowUI;
    public WeaponDetailsShowUI secondWeaponDetailsShowUI;
    public CharacterSkillDetailsShowUI characterSkillDetailsShowUI;
    
    [Header("Setting")]
    public Color valueLessColor = Color.red;
    public Color valueHighColor = Color.green;
    public Color valueNormalColor = Color.white;

    #region Event

    private void OnEnable()
    {
        EventHandler.ClickCharacterToggle += OnClickCharacterToggle; // Set current character details
    }

    private void OnDisable()
    {
        EventHandler.ClickCharacterToggle -= OnClickCharacterToggle;
    }

    private void OnClickCharacterToggle(CharacterShowDetails data)
    {
        characterDetails = data.characterDetails;
        SetCharacterDetails();
    }
    
    #endregion 
    
    private void SetCharacterDetails()
    {
        // Character Details
        characterDetailsShowUI.characterNameText.text = characterDetails.characterName;
        SetTextWithData(ref characterDetailsShowUI.characterHealthText, characterDetails.startHealth, 100, 100);
        SetTextWithData(ref characterDetailsShowUI.characterDefenseText, characterDetails.startDefense, 50, 50);
        characterDetailsShowUI.characterDefenseSlider.value = characterDetails.startDefense;
        SetTextWithData(ref characterDetailsShowUI.characterRecoveryText, characterDetails.recoveryHealth, 30, 30);
        
        // Main Weapon Details
        mainWeaponDetailsShowUI.weaponNameText.text = characterDetails.mainWeaponDetails.weaponName;
        mainWeaponDetailsShowUI.weaponShootModeText.text = characterDetails.mainWeaponDetails.gunShootMode == GunShootMode.semi_auto? "半自動" : "全自動";
        mainWeaponDetailsShowUI.weaponImage.sprite = characterDetails.mainWeaponDetails.weaponSprite;
        SetTextWithData(ref mainWeaponDetailsShowUI.clipBulletCountText, characterDetails.mainWeaponDetails.clipBulletCount, 24, 29);
        SetTextWithData(ref mainWeaponDetailsShowUI.bagBulletCountText, characterDetails.mainWeaponDetails.bagBulletCount, 100, 150);
        SetTextWithData(ref mainWeaponDetailsShowUI.weaponDamageText, characterDetails.mainWeaponDetails.damage, 40, 60);
        SetTextWithData(ref mainWeaponDetailsShowUI.shootCooldownText, characterDetails.mainWeaponDetails.shootCooldown, 0.05f, 0.1f, true);
        SetTextWithData(ref mainWeaponDetailsShowUI.reloadTimeText, characterDetails.mainWeaponDetails.weaponReloadTime, 1.5f, 2.5f, true);
        mainWeaponDetailsShowUI.shootRangeText.text =
            $"±{characterDetails.mainWeaponDetails.minShootAngle} ~ ±{characterDetails.mainWeaponDetails.maxShootAngle}";
        
        SetTextWithData(ref mainWeaponDetailsShowUI.cameraSightText, characterDetails.mainWeaponDetails.cameraSight, 6.5f, 6.5f);
        SetTextWithData(ref mainWeaponDetailsShowUI.lightOutSightText, characterDetails.mainWeaponDetails.lightOutSight, 7.2f, 7.2f);
        SetTextWithData(ref mainWeaponDetailsShowUI.fireCameraShakeText, characterDetails.mainWeaponDetails.fireCameraShake, 7, 7, true);
        
        // Second Weapon Details
        secondWeaponDetailsShowUI.weaponNameText.text = characterDetails.secondWeaponDetails.weaponName;
        secondWeaponDetailsShowUI.weaponShootModeText.text = characterDetails.secondWeaponDetails.gunShootMode == GunShootMode.semi_auto? "半自動" : "全自動";
        secondWeaponDetailsShowUI.weaponImage.sprite = characterDetails.secondWeaponDetails.weaponSprite;
        SetTextWithData(ref secondWeaponDetailsShowUI.clipBulletCountText, characterDetails.secondWeaponDetails.clipBulletCount, 8, 10);
        SetTextWithData(ref secondWeaponDetailsShowUI.bagBulletCountText, characterDetails.secondWeaponDetails.bagBulletCount, 50, 60);
        SetTextWithData(ref secondWeaponDetailsShowUI.weaponDamageText, characterDetails.secondWeaponDetails.damage, 40, 45);
        SetTextWithData(ref secondWeaponDetailsShowUI.shootCooldownText, characterDetails.secondWeaponDetails.shootCooldown, 0.2f, 0.3f, true);
        SetTextWithData(ref secondWeaponDetailsShowUI.reloadTimeText, characterDetails.secondWeaponDetails.weaponReloadTime, 0.8f, 1.2f, true);
        secondWeaponDetailsShowUI.shootRangeText.text =
            $"±{characterDetails.secondWeaponDetails.minShootAngle} ~ ±{characterDetails.secondWeaponDetails.maxShootAngle}";
        
        SetTextWithData(ref secondWeaponDetailsShowUI.cameraSightText, characterDetails.secondWeaponDetails.cameraSight, 5.5f, 5.5f);
        SetTextWithData(ref secondWeaponDetailsShowUI.lightOutSightText, characterDetails.secondWeaponDetails.lightOutSight, 7.78f, 7.78f);
        SetTextWithData(ref secondWeaponDetailsShowUI.fireCameraShakeText, characterDetails.secondWeaponDetails.fireCameraShake, 10, 10, true);

        // Character Skill Details
        characterSkillDetailsShowUI.skillNameText.text = characterDetails.skillItemName;
        characterSkillDetailsShowUI.skillImage.sprite = characterDetails.skillItemSprite;
        characterSkillDetailsShowUI.skillDescriptionText.text = characterDetails.skillDescription;
    }

    /// <summary>
    /// Set Text and Color with data value
    /// </summary>
    /// <param name="targetText">The target text about you want change</param>
    /// <param name="dataValue">The value about tou want display on UI</param>
    /// <param name="less">Confirm data values are lower than average</param>
    /// <param name="high">Confirm data values are higher than average</param>
    /// <param name="colorReverse">If higher values are worse for results, turn this setting on,
    /// it will invert the color setting</param>
    private void SetTextWithData(ref TextMeshProUGUI targetText, float dataValue, float less, float high, bool colorReverse = false)
    {
        // Set Text
        targetText.text = dataValue.ToString(CultureInfo.InvariantCulture);
        
        // Set Color
        if(dataValue < less)
            targetText.color = colorReverse? valueHighColor : valueLessColor;
        else if(dataValue > high)
            targetText.color = colorReverse? valueLessColor : valueHighColor;
        else
            targetText.color = valueNormalColor;
    }
    
}
