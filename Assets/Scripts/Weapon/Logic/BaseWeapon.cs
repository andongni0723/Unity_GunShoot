using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class BaseWeapon : MonoBehaviour
{
    [Header("Object")]
    public List<WeaponDetails_SO> weaponList;

    public WeaponDetails_SO currentWeapon;

    [Header("Component")]
    public Light2D weaponLight;
    
    public BulletPool bulletPool;

    public int currentBulletCount;
    public int currentBagBulletCount;

    private int reloadBulletCount;
    
    //Timer
    [SerializeField]
    protected float timer = 0;
    [SerializeField]
    protected bool isTimerEnd = true;
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
        if (!isTimerEnd)
        {
           
            timer += Time.deltaTime;
    
            if (timer >= endTime)
            {
                isTimerEnd = true;
            }
        }
    }


    public void Fire(GameObject fireObject, GameObject firePoint)
    {
        // Check not reloading
        if (isTimerEnd)
            StartCoroutine(FireAction(fireObject, firePoint));
    }
    
    IEnumerator FireAction(GameObject fireObject, GameObject firePoint)
    {
        if (currentBulletCount > 0)
        {
            float rotationZ = fireObject.transform.rotation.eulerAngles.z;
            AudioManager.Instance.PlayAudio(currentWeapon.fireAudio);
            CinemachineShake.Instance.CameraShake(10, 0.1f);
            bulletPool.Fire(firePoint.transform.position, 
                Quaternion.Euler(0,0, Random.Range(rotationZ - weaponLight.pointLightInnerAngle / 2, rotationZ + weaponLight.pointLightInnerAngle / 2)));

            // Check the bullet count
            currentBulletCount--;
        }
        else
        {
            // TODO: Reload
            

            StartCoroutine(ReloadBullet());
        }

        yield return null;
    }

    public void SpeedToChangeShootAngle(float speed)
    {
        float angle = currentWeapon.minShootAngle +
                      speed * (currentWeapon.maxShootAngle - currentWeapon.minShootAngle);
        
        weaponLight.pointLightInnerAngle = angle;
        weaponLight.pointLightOuterAngle = angle;
    }

    public virtual IEnumerator ReloadBullet()
    {
        if (currentBagBulletCount > 0)
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
            CallTimer(currentWeapon.weaponReloadTime);
            yield return new WaitUntil(() => isTimerEnd );

            currentBagBulletCount -= reloadBulletCount;
            currentBulletCount += reloadBulletCount; 
        }
    }

    private void CallTimer(float endTime)
    {
        isTimerEnd = false;
        timer = 0; 
        this.endTime = endTime;
    }
}
