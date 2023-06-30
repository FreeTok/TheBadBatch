using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowProjectile : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    protected float _speed;
    
    protected Rigidbody rb;
    
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
        
        rb.velocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.isKinematic = true;
    }
}
