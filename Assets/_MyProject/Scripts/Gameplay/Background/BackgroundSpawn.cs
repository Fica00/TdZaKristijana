using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawn : MonoBehaviour
{
    Vector3 previousLocalPos;
    public Transform camera;

    void Update()
    {
        TeleportBackground();
    }

    void TeleportBackground()
    {
        if (transform.position.x - camera.localPosition.x < -21f)
        {
            previousLocalPos = transform.localPosition;
            transform.localPosition = new Vector3(transform.localPosition.x + 2 * 6.336f, transform.localPosition.y);
        }

        if (transform.position.x - camera.localPosition.x > 21f)
        {
            transform.localPosition = previousLocalPos;
        }

        //print(transform.position.x - camera.localPosition.x + gameObject.name);
    }
}
