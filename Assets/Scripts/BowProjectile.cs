using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowProjectile : WeaponProjectile
{
    public void SetupBullet(float newDamage, float speed)
    {
        damage = newDamage;
        _speed = speed;
        
        Go();
    }
    
    private void Go()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        
        rb.velocity = transform.forward * _speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet"))
        {
            return;
        }
        
        print(other);
        transform.SetParent(other.transform);

        Destroy(rb);
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player") || other.CompareTag("Bullet"))
    //     {
    //         return;
    //     }
    //     print(other.gameObject);
    //     print("triggered");
    //     rb.isKinematic = true;
    //     rb.constraints = RigidbodyConstraints.FreezeAll;
    // }
}
