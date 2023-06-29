using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class ThirdPersonShooterControllerBehaviour : MonoBehaviour
{
    [Header("Shooting")] 
    public WeaponController weaponController;
    [SerializeField] protected CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] protected float normalSensitivity = 1f;
    [SerializeField] protected float aimSensitivity = 0.5f;
    [SerializeField] protected LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] protected Rig aimRig;
    [SerializeField] protected Transform aimDir;
    
    [Header("Characters")]
    [SerializeField] protected GameObject changeCharPanel;
    [SerializeField] protected GameObject[] buttons;
    [SerializeField] protected Transform cameraRoot;
    [SerializeField] protected CinemachineVirtualCamera[] cameras;
    public CircularMenuController changeChar;


    protected ThirdPersonController thirdPersonController;
    protected StarterAssetsInputs starterAssetsInputs;
    protected Animator animator;
    
    private bool switchingCharacter = false;

    protected Vector3 _mouseWorldPosition;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            aimDir.position = raycastHit.point;
            _mouseWorldPosition = raycastHit.point;
        }
        
        if (starterAssetsInputs.aim)
        {
            Aim();
        }
        else
        {
            DisAim();
        }

        if (starterAssetsInputs.shoot)
        {
            Shoot();
        }
        
        changeCharPanel.SetActive(starterAssetsInputs.characterSwitch);
        if (starterAssetsInputs.characterSwitch)
        {
            switchingCharacter = true;

            var choose = changeChar.CalculateChoose();
            
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

            //TODO
            print(changeChar.lastElem);
            Events.OnCharacterChanged?.Invoke((CharactersController.Characters) changeChar.lastElem);

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

    public void InitializeCameras()
    {
        foreach (var cam in cameras)
        {
            cam.Follow = cameraRoot;
        }
    }

    protected virtual void Aim()
    {
        
    }

    protected virtual void DisAim()
    {
        
    }

    protected virtual void Shoot()
    {

    }
}
