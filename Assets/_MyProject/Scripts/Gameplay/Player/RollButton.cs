using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RollButton : MonoBehaviour, IPointerDownHandler
{
    public static bool isRollPressed;
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        isRollPressed = true;
    }
}
