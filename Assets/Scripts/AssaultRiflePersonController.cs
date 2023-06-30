using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRiflePersonController : ThirdPersonShooterControllerBehaviour
{
    [Header("Detector Ability")]
    [SerializeField] private float detectorLength = 20f;
    [SerializeField] private float detectionTime = 5f;
    
    protected override void Aim()
    {
        aimVirtualCamera.gameObject.SetActive(true);
        thirdPersonController.SetSensitivity(aimSensitivity);
        thirdPersonController.SetRotateOnMove(false);
        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

        Vector3 worldAimTarget = _mouseWorldPosition;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

        aimRig.weight = Mathf.Lerp(aimRig.weight, 1f, Time.deltaTime * 20f);
    }

    protected override void DisAim()
    {
        aimVirtualCamera.gameObject.SetActive(false);
        thirdPersonController.SetSensitivity(normalSensitivity);
        thirdPersonController.SetRotateOnMove(true);
        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            
        aimRig.weight = Mathf.Lerp(aimRig.weight, 0f, Time.deltaTime * 20f);
    }

    protected override void Shoot()
    {
        weaponController.Shoot(_mouseWorldPosition);
        starterAssetsInputs.shoot = false;
    }

    protected override void FirstAbil()
    {
        print("FirstAbil");
        starterAssetsInputs.firstAbil = false;

        var allEnemies = FindObjectsOfType<EnemyController>();

        foreach (var enemy in allEnemies)
        {
            if ((enemy.transform.position - transform.position).magnitude <= detectorLength)
            {
                enemy.Outline(detectionTime);
            }
        }
        //TODO scan
    }
    
    protected override void SecondAbil()
    {
        //TODO Defender
    }
}
