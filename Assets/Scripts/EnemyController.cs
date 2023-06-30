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

    private Outline _outline;

    private void Awake()
    {
        _getDamages = GetComponentsInChildren<GetDamage>();
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _outline = GetComponent<Outline>();
    }

    private void Start()
    {
        _outline.enabled = false;
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
        
        EnemyMovement toDestroy = GetComponent<EnemyMovement>();
        toDestroy.StopMove();
        Destroy(toDestroy);
        Destroy(GetComponent<Animator>());
        Destroy(_outline);
        
        Destroy(this);
    }

    public void Outline(float time)
    {
        Invoke(nameof(DisableOutline), time);
        
        _outline.enabled = true;
    }

    private void DisableOutline()
    {
        _outline.enabled = false;
    }
}
