using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler
{
    public static bool isJumpPressed;
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        isJumpPressed = true;
    }

}

