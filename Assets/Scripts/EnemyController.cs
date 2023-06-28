using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxHealth = 100f;
    private float _health;

    private GetDamage[] _getDamages;
    private Rigidbody[] _rigidbodies;

    private void Awake()
    {
        _getDamages = GetComponentsInChildren<GetDamage>();
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    private void Start()
    {
        _health = maxHealth;

        foreach (var getDamage in _getDamages)
        {
            getDamage.controller = this;
        }

        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void GetDamage(float damage)
    {
        _health = Math.Clamp(_health - damage, 0f, 100f);

        if (_health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        print("Died");
        
        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.constraints = RigidbodyConstraints.None;
        }
        
        Destroy(this);
    }
}
