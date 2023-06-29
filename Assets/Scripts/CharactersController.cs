using System;
using StarterAssets;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class CharactersController : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] private GameObject[] characters;
    public enum Characters
    {
        SSR = 0,
        Bow = 1,
        Weapon = 2,
        Weapon1 = 3,
        Weapon2 = 4
    }
    public Characters startCharacter;
    private Characters _activeCharacter;
    
    private void Awake()
    {
        Events.OnCharacterChanged.AddListener(ChangeCharacter);
    }

    private void Start()
    {
        SwitchCharactersComponents(Characters.SSR, false);
        SwitchCharactersComponents(Characters.Bow, false);
        SwitchCharactersComponents(Characters.Weapon, false);
        SwitchCharactersComponents(Characters.Weapon1, false);
        SwitchCharactersComponents(Characters.Weapon2, false);
        
        _activeCharacter = SwitchCharactersComponents(startCharacter, true);
    }

    private void ChangeCharacter(Characters newCharacter)
    {
        print(newCharacter);
        SwitchCharactersComponents(_activeCharacter, false);
        _activeCharacter = SwitchCharactersComponents(newCharacter, true);
    }

    private Characters SwitchCharactersComponents(Characters character, bool enabling)
    {
        var characterGameObject = characters[(int) character];
        
        characterGameObject.GetComponent<CharacterController>().enabled = enabling;
        characterGameObject.GetComponent<ThirdPersonController>().enabled = enabling;
        characterGameObject.GetComponent<PlayerInput>().enabled = enabling;
        characterGameObject.GetComponent<ThirdPersonShooterControllerBehaviour>().enabled = enabling;
        characterGameObject.GetComponent<RigBuilder>().enabled = enabling;
        
        if (enabling)
        {
            characterGameObject.GetComponent<ThirdPersonController>().Initialize();
            characterGameObject.GetComponent<PlayerInput>().ActivateInput();
            
            characterGameObject.GetComponent<ThirdPersonShooterControllerBehaviour>().InitializeCameras();
        }
        else
        {
            characterGameObject.GetComponent<PlayerInput>().DeactivateInput();
        }

        return character;
    }
}
