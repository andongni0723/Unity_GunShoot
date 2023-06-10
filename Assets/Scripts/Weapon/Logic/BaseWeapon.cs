using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BaseWeapon : MonoBehaviour
{
    [Header("Object")]
    public List<WeaponDetails_SO> weaponList;
    public WeaponDetails_SO currentWeapon;
    public WeaponBulletData data;

    [Header("Component")]
    public Light2D weaponLight;
    public BulletPool bulletPool;

    [Header("Setting")]
    public float cameraShakeIntensity = 10;

    // The action will be set by PlayerWeapon (Enemy not to use)
    private Action saveWeaponDataAction;
    private Action loadWeaponDataAction;
    private Action weaponReloadAction;
    private Action weaponReloadEndAction;

    //var
    private int reloadBulletCount;
    
    [Space(15f)]
    [FoldoutGroup("Debug", true)]public int currentBulletCount;
    [FoldoutGroup("Debug")] public int currentBagBulletCount;
    [FoldoutGroup("Debug"), SerializeField] protected float currentAngle;
    
    //Timer
    [Space(10f)]
    [FoldoutGroup("Debug"), SerializeField] protected float reloadTimer = 0;
    [FoldoutGroup("Debug"), SerializeField] public bool isReloadTimerEnd = true;
    [FoldoutGroup("Debug"), SerializeField] public bool isReloadEnd = true;
    [FoldoutGroup("Debug"), SerializeField] protected float shootCooldownTimer = 0;
    [FoldoutGroup("Debug"), SerializeField] public bool isShootCooldownTimerEnd = true;

    protected float endTime;

    protected virtual void Awake()
    {
        currentWeapon = weaponList[0];

        // New bullet data from currentWeapon
        data =
            new WeaponBulletData(currentWeapon, currentWeapon.clipBulletCount, currentWeapon.bagBulletCount);
        
        // currentBulletCount = currentWeapon.clipBulletCount;
        // currentBagBulletCount = currentWeapon.bagBulletCount;
        
    }

    protected virtual void Update()
    {
        // Timer
        if (!isReloadTimerEnd)
        {
            reloadTimer += Time.deltaTime;
    
            if (reloadTimer >= endTime)
            {
                isReloadTimerEnd = true;
            }
        }

        if (!isShootCooldownTimerEnd)
        {
            shootCooldownTimer += Time.deltaTime;
    
            if (shootCooldownTimer >= endTime)
            {
                isShootCooldownTimerEnd = true;
            }
        }
    }


    public void Fire(GameObject fireObject, GameObject firePoint)
    {
        // Check not reloading
        if (isReloadEnd)
            StartCoroutine(FireAction(fireObject, firePoint));
    }
    
    IEnumerator FireAction(GameObject fireObject, GameObject firePoint)
    {
        // Check isn't in shoot cooldown
        if (isShootCooldownTimerEnd && isReloadEnd)
        {
            // Check have bullet
            if (data.currentBulletCount > 0)
            {
                float rotationZ = fireObject.transform.rotation.eulerAngles.z;
                AudioManager.Instance.PlayAudio(data.weaponDetails.fireAudio);
                CameraController.Instance.CameraShake(data.weaponDetails.fireCameraShake, 0.1f);
                bulletPool.Fire(firePoint.transform.position, 
                    Quaternion.Euler(0,0, Random.Range(rotationZ - currentAngle / 2, rotationZ + currentAngle / 2)), currentWeapon.damage, gameObject.layer);
                
                data.currentBulletCount--;
                CallShootCooldownTimer(data.weaponDetails.shootCooldown);
                saveWeaponDataAction?.Invoke(); // Save data

            }
            else
            {
                // TODO: Reload

                StartCoroutine(ReloadBullet()); 
            }

        }
        yield return null;
    }

    public virtual IEnumerator ReloadBullet()
    {
        if (data.currentBagBulletCount > 0 && isReloadTimerEnd)
        {
            // Check Bag bullet is enough for a clip 
            if (data.weaponDetails.clipBulletCount - data.currentBulletCount < data.currentBagBulletCount)
            {
                reloadBulletCount = data.weaponDetails.clipBulletCount - data.currentBulletCount;
            }
            else
            {
                // Check bag bullet have
                if (currentBagBulletCount > 0)
                {
                    reloadBulletCount = data.currentBagBulletCount;

                }
            }

            // Wait Reload Bullet time
            CallReloadTimer(data.weaponDetails.weaponReloadTime);
            weaponReloadAction?.Invoke(); // Reload Action
            yield return new WaitUntil(() => isReloadTimerEnd );
            
            data.currentBagBulletCount -= reloadBulletCount;
            data.currentBulletCount += reloadBulletCount;
            saveWeaponDataAction?.Invoke(); // Save data
            isReloadEnd = true;
            weaponReloadEndAction?.Invoke(); // Reload End Action
        }
    }

    /// <summary>
    /// The angle of shoot will be change by character speed
    /// </summary>
    /// <param name="speed"></param>
    public virtual void SpeedToChangeShootAngle(float speed)
    {
        currentAngle = currentWeapon.minShootAngle +
                       speed * (currentWeapon.maxShootAngle - currentWeapon.minShootAngle);
    }

    private void CallReloadTimer(float _endTime)
    {
        isReloadTimerEnd = false;
        isReloadEnd = false;
        reloadTimer = 0; 
        endTime = _endTime;
    }

    protected void CallShootCooldownTimer(float _endTime)
    {
        isShootCooldownTimerEnd = false;
        shootCooldownTimer= 0;
        endTime = _endTime;
    }

    protected  void SetSaveBulletDataAction(Action action)
    {
        saveWeaponDataAction = action;
    }

    protected  void SetLoadBulletDataAction(Action action)
    {
        loadWeaponDataAction = action;
    }
    
    protected void SetWeaponReloadAction(Action action)
    {
        weaponReloadAction = action;
    }
    protected void SetWeaponReloadEndAction(Action action)
    {
        weaponReloadEndAction = action;
    }
}
