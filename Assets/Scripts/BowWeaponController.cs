using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowWeaponController : WeaponController
{
    private Transform _arrow;

    private void Start()
    {
        InstantiateArrow();
    }

    private void InstantiateArrow()
    {
        _arrow = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, muzzleTransform);
        _arrow.transform.localPosition = Vector3.zero;
        _arrow.transform.localRotation = Quaternion.identity;
    }

    public void Shoot(float damageMult)
    {
        print(damageMult * damage);
        _arrow.parent = null;
        _arrow.GetComponent<BowProjectile>().SetupBullet(damage * damageMult, bulletSpeed);

        InstantiateArrow();
    }
}
