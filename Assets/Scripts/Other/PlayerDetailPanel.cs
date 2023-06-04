using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerDetailPanel : MonoBehaviour
{
    private PlayerInputControlls inputControlls;

    [Header("Component")] 
    public GameObject panelParent;
    public WeaponToggle mainWeaponToggle;
    public WeaponToggle secondWeaponToggle;

    private void Awake()
    {
        
        //inputControlls.UI.Click.performed += _ => OnCancel();
        //inputControlls.GamePlay.Fire.performed += _ => OnCancel();
        
        panelParent.SetActive(false);
    }

    private void Start()
    {
        //inputControlls = PlayerController.Instance.inputControlls;

        //inputControlls.UI.Cancel.performed += _ => OnCancel();
        
        //inputControlls.GamePlay.PlayerDetails.performed += _ => OnOpen();
    }

    private void OnOpen() => Debug.Log("TAB Open");

    #region Event

    private void OnEnable()
    {
        EventHandler.ReadPlayDetail += OnReadPlayDetail; // Update detail to componemts
    }

    private void OnDisable()
    {
        EventHandler.ReadPlayDetail -= OnReadPlayDetail;
    }

    private void OnReadPlayDetail(WeaponDetails_SO mainWeapon, WeaponDetails_SO secondWeapon, WeaponDetails_SO currentWeapon)
    {
        if (!panelParent.activeSelf)
        {
            mainWeaponToggle.weaponDetails = mainWeapon;
            secondWeaponToggle.weaponDetails = secondWeapon;
            Debug.Log("TAB1");
            panelParent.SetActive(true);
    
            EventHandler.CallUpdatePlayerDetails(currentWeapon);
        }
        else
        {
            OnCancel();
        }
        
    }

    #endregion 

    public void OnCancel()
    {
        panelParent.SetActive(false);
    }

}
