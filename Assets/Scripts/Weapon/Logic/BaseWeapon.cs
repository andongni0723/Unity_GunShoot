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

    [Header("Component")]
    public Light2D weaponLight;
    public BulletPool bulletPool;

    [Header("Setting")]
    public float cameraShakeIntensity = 10;

    //var
    private int reloadBulletCount;
    
    [Space(15f)]
    [FoldoutGroup("Debug", true)]public int currentBulletCount;
    [FoldoutGroup("Debug")] public int currentBagBulletCount;
    [FoldoutGroup("Debug"), SerializeField] protected float currentAngle;
    
    //Timer
    [Space(10f)]
    [FoldoutGroup("Debug"), SerializeField] protected float reloadTimer = 0;
    [FoldoutGroup("Debug"), SerializeField] protected bool isReloadTimerEnd = true;
    [FoldoutGroup("Debug"), SerializeField] protected float shootCooldownTimer = 0;
    [FoldoutGroup("Debug"), SerializeField] protected bool isShootCooldownTimerEnd = true;

    protected float endTime;

    private void Awake()
    {
        currentWeapon = weaponList[0];
        
        currentBulletCount = currentWeapon.clipBulletCount;
        currentBagBulletCount = currentWeapon.bagBulletCount;
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
        if (isReloadTimerEnd)
            StartCoroutine(FireAction(fireObject, firePoint));
    }
    
    IEnumerator FireAction(GameObject fireObject, GameObject firePoint)
    {
        // Check isn't in shoot cooldown
        if (isShootCooldownTimerEnd && isReloadTimerEnd)
        {
            // Check have bullet
            if (currentBulletCount > 0)
            {
                float rotationZ = fireObject.transform.rotation.eulerAngles.z;
                AudioManager.Instance.PlayAudio(currentWeapon.fireAudio);
                CinemachineShake.Instance.CameraShake(10, 0.1f);
                bulletPool.Fire(firePoint.transform.position, 
                    Quaternion.Euler(0,0, Random.Range(rotationZ - currentAngle / 2, rotationZ + currentAngle / 2)), currentWeapon.damage, gameObject.layer);
                
                currentBulletCount--;
                CallShootCooldownTimer(currentWeapon.shootCooldown);
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
        if (currentBagBulletCount > 0 && isReloadTimerEnd)
        {
            // Check Bag bullet is enough for a clip 
            if (currentWeapon.clipBulletCount - currentBulletCount < currentBagBulletCount)
            {
                reloadBulletCount = currentWeapon.clipBulletCount - currentBulletCount;
            }
            else
            {
                // Check bag bullet have
                if (currentBagBulletCount > 0)
                {
                    reloadBulletCount = currentBagBulletCount;
                }
            }

            // Wait Reload Bullet time
            CallReloadTimer(currentWeapon.weaponReloadTime);
            yield return new WaitUntil(() => isReloadTimerEnd );

            currentBagBulletCount -= reloadBulletCount;
            currentBulletCount += reloadBulletCount; 
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
        reloadTimer = 0; 
        endTime = _endTime;
    }

    protected void CallShootCooldownTimer(float _endTime)
    {
        isShootCooldownTimerEnd = false;
        shootCooldownTimer= 0;
        endTime = _endTime;
    }
}
