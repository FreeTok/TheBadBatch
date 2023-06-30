using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    protected float _speed;
    
    protected Rigidbody rb;
    
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

    protected virtual void Go()
    {
        rb.velocity = transform.forward * _speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
