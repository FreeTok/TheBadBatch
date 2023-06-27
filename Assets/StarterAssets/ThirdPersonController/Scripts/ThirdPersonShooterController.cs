using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity = 1f;
    [SerializeField] private float aimSensitivity = 0.5f;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform bulletProjectile;
    [SerializeField] private Transform muzzleTransform;
    [SerializeField] private Rig aimRig;
    [SerializeField] private GameObject changeCharPanel;
    [SerializeField] private GameObject[] buttons;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private CircularMenuController _changeChar;
    
    public bool switchingCharacter = false;
    
    

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        _changeChar = FindObjectOfType<CircularMenuController>();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }
        
        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

            aimRig.weight = Mathf.Lerp(aimRig.weight, 1f, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            
            aimRig.weight = Mathf.Lerp(aimRig.weight, 0f, Time.deltaTime * 20f);
        }

        if (starterAssetsInputs.shoot)
        {
            Vector3 aimDir = (mouseWorldPosition - muzzleTransform.position).normalized;
            Instantiate(bulletProjectile, muzzleTransform.position, Quaternion.LookRotation(aimDir, Vector3.up));
            starterAssetsInputs.shoot = false;
        }
        
        changeCharPanel.SetActive(starterAssetsInputs.characterSwitch);
        if (starterAssetsInputs.characterSwitch)
        {
            switchingCharacter = true;

            var choose = _changeChar.CalculateChoose();
            
            if (choose != -1)
            {
                foreach (var button in buttons)
                {
                    if (button == buttons[choose])
                    {
                        button.GetComponent<Image>().color = Color.blue;
                    }

                    else
                    {
                        button.GetComponent<Image>().color = Color.white;
                    }
                }
            }
        }
        
        else if (switchingCharacter)
        {
            // Switch
            print("switch");
            switchingCharacter = false;
            
            print(_changeChar.CalculateChoose());

            // var pos = (new Vector2(Input.mousePosition.x, Input.mousePosition.y) - screenCenterPoint).normalized;
            // List<float> dists = new List<float>();
            //
            // foreach (var button in buttons)
            // {
            //     var dist = (new Vector2(button.transform.position.x, button.transform.position.y) - pos).magnitude;
            //     dists.Add(dist);
            // }
            //
            // print(dists.Max());
            // Destroy(buttons[dists.IndexOf(dists.Max())]);
        }
        
    }
}
