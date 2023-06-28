using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float damage = 50f;
    [SerializeField] private float bulletSpeed = 100f;
    
    [SerializeField] private Transform bulletPrefab;
    public Transform muzzleTransform;
    
    
    public void Shoot(Vector3 direction)
    {
        Vector3 aimDir = (direction - muzzleTransform.position).normalized;
        var bullet = Instantiate(bulletPrefab, muzzleTransform.position, Quaternion.LookRotation(aimDir, Vector3.up));

        bullet.GetComponent<BulletProjectile>().SetupBullet(damage, bulletSpeed);
    }
}
