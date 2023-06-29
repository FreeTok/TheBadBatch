using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowPersonController : ThirdPersonShooterControllerBehaviour
{
    [SerializeField]
    private float _bowStrength;

    [SerializeField] private float pullingSpeed = 5f;
        
    protected override void Aim()
    {
        _bowStrength = Mathf.Clamp01(_bowStrength + Time.deltaTime * pullingSpeed);
    }

    protected override void DisAim()
    {
        _bowStrength = Mathf.Clamp01(_bowStrength - Time.deltaTime * pullingSpeed);
    }

    protected override void Shoot()
    {
        weaponController.Shoot(_mouseWorldPosition);
        starterAssetsInputs.shoot = false;
    }
}
