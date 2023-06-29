using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events
{
    public static UnityEvent<CharactersController.Characters> OnCharacterChanged = new UnityEvent<CharactersController.Characters>();
    
}
