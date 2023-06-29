using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class CircularMenuController : MonoBehaviour
{
    [HideInInspector]
    public StarterAssetsInputs _input;
    public int itemCount = 5;
    public int buffer = 10;
    
    
    private int activeCharacterIndex;
    private Vector2 mouseMovement;

    public int lastElem = 1;


    public int CalculateChoose()
    {
        if (_input.look.magnitude < 0.1)
        {
            return -1;
        }
        
        float angle = Mathf.Atan2(_input.look.normalized.y, _input.look.normalized.x) / Mathf.PI;
        angle *= 180;
        angle += 90f;

        if (angle < 0)
        { 
            angle += 360; 
        }
            
        float angleStep = 360f / itemCount;
        int selectedElement = Mathf.RoundToInt(angle / angleStep);
        

        // if (lastElem != Mathf.RoundToInt((angle + buffer) / angleStep) ||
        //                                     lastElem != Mathf.RoundToInt((angle - buffer) / angleStep))
        // {
        //     print(angle + " | " + Mathf.RoundToInt((angle + buffer) / angleStep) + " | " + Mathf.RoundToInt((angle - buffer) / angleStep));
        //     return -1;
        // }

        print(angle);

        if (lastElem != -1)
        {
            lastElem = selectedElement;
        }

        return selectedElement;

            // for (int i = 0; i < itemCount; i++)
            // {
            //     if (angle > i * (360 / itemCount) && angle < (i + 1) * (360 / itemCount))
            //     {
            //         return i;
            //     }
            // }
        

        // mouseMovement = mouseMovement.normalized;

        // print(mouseMovement);

        // var selected = (int)(5 * Math.Atan2(mouseMovement.y, mouseMovement.x) * Mathf.Rad2Deg / 180);
        // print(selected);
        //
        // float angle = Mathf.Atan2(mouseMovement.y, mouseMovement.x) * Mathf.Rad2Deg;
        //
        // float normalizedValue = Mathf.InverseLerp(-180, 180, angle);
        // var newAngle = Mathf.Lerp(0, 360, normalizedValue);
        //

        //
        // // print(newAngle);
        // lastElem = selectedElement;
        // return selected;
    }
}
