using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    [HideInInspector]
    public float damage;

    protected float _speed;

    protected Rigidbody rb;
}
