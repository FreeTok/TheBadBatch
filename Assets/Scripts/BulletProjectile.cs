using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : WeaponProjectile
{
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetupBullet(float newDamage, float speed)
    {
        damage = newDamage;
        _speed = speed;
        
        Go();
    }

    private void Go()
    {
        rb.velocity = transform.forward * _speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
