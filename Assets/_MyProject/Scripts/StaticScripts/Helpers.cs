using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helpers
{
    private static PointerEventData eventDataCurrentPosition;
    private static List<RaycastResult> _results;


    //returns true if cursor is over UI item
    public static bool IsOverUI()
    {
        eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                eventDataCurrentPosition.position = Input.GetTouch(0).position;
            }
            else
            {
                return false;
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WebGLPlayer)
        {
            eventDataCurrentPosition.position = Input.mousePosition;
        }
        else
        {
            throw new System.Exception("Cant get position for platform: " + Application.platform);
        }

        _results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, _results);
        return _results.Count > 0;
    }

    //usecase:
    //transform.position = Helpers.GetWorlPositionOfCanvasElement(followTarget)
    //this would render transform.gameObject behind element (good usecase for fireworks or something like that)
    public static Vector2 GetWorlPositionOfCanvasElement(RectTransform _element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(_element, _element.position, Camera.main, out var result);
        return result;
    }

    public static void DeleteChildren(this Transform _transform)
    {
        foreach (Transform child in _transform)
        {
            Object.Destroy(child.gameObject);
        }
    }

    public static void ClearObjectsAndList<T>(this List<T> _list) where T : MonoBehaviour
    {
        foreach (var _element in _list)
        {
            Object.Destroy(_element.gameObject);
        }

        _list.Clear();
    }
}
