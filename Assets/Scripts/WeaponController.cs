using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] protected float damage = 50f;
    [SerializeField] protected float bulletSpeed = 100f;
    
    [SerializeField] protected Transform bulletPrefab;
    [SerializeField] protected Transform muzzleTransform;
    
    
    public void Shoot(Vector3 direction)
    {
        Vector3 aimDir = (direction - muzzleTransform.position).normalized;
        var bullet = Instantiate(bulletPrefab, muzzleTransform.position, Quaternion.LookRotation(aimDir, Vector3.up));

        bullet.GetComponent<BulletProjectile>().SetupBullet(damage, bulletSpeed);
    }
}
