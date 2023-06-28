using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDamage : MonoBehaviour
{
    [HideInInspector] public EnemyController controller;
    public float damageMultiplayer = 1f;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            print("Damage");
            controller.GetDamage(other.gameObject.GetComponent<BulletProjectile>().damage * damageMultiplayer);
        }
    }
}
