using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowPersonController : ThirdPersonShooterControllerBehaviour
{
    private float _bowStrength;

    [SerializeField] private float pullingSpeed = 5f;

    private bool _isDisablingCoroutineStarted, _isEnablingCoroutineStarted;
        
    protected override void Aim()
    {
        aimVirtualCamera.gameObject.SetActive(true);
        thirdPersonController.SetRotateOnMove(false);
        animator.SetBool("Aiming", true);
        
        StopCoroutine(DisableShootIK());
        _isDisablingCoroutineStarted = false;
        
        Vector3 worldAimTarget = _mouseWorldPosition;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        
        float _speed = 20f / (1 - aimRig.weight);

        if (aimRig.weight != 1 && !_isEnablingCoroutineStarted)
        {
            StopAllCoroutines();
            StartCoroutine(EnableShootIK(_speed));
            _isEnablingCoroutineStarted = true;
        }
        // aimRig.weight = Mathf.Lerp(aimRig.weight, 1f, Time.deltaTime * _speed);
        
        _bowStrength = Mathf.Clamp01(_bowStrength + Time.deltaTime * pullingSpeed);

    }

    protected override void DisAim()
    {
        aimVirtualCamera.gameObject.SetActive(false);
        thirdPersonController.SetRotateOnMove(true);
        animator.SetBool("Aiming", false);
        
        if (aimRig.weight != 0 && !_isDisablingCoroutineStarted)
        {
            StopAllCoroutines();
            StartCoroutine(DisableShootIK());
            _isDisablingCoroutineStarted = true;
        }
    }

    IEnumerator EnableShootIK(float speed)
    {
        while (aimRig.weight < 0.99)
        {
            aimRig.weight = Mathf.Lerp(aimRig.weight, 1f, Time.deltaTime * speed);
            yield return null;
        }

        aimRig.weight = 1;
        _isEnablingCoroutineStarted = false;
    }

    IEnumerator DisableShootIK()
    {
        weaponController.GetComponent<BowWeaponController>().Shoot(_bowStrength);
        _bowStrength = 0;

        yield return new WaitForSeconds(0.67f);

        while (aimRig.weight > 0.01)
        {
            aimRig.weight = Mathf.Lerp(aimRig.weight, 0f, Time.deltaTime * 20f);
            yield return null;
        }

        aimRig.weight = 0;
        _isDisablingCoroutineStarted = false;
        
    }
}
