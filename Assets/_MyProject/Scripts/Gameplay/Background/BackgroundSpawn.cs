using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawn : MonoBehaviour
{
    Vector3 previousLocalPos;

    void Update()
    {
        TeleportBackground();

    }

    void TeleportBackground()
    {
        if (PlayerManager.player.transform.position.x - transform.position.x > 14f)
        {
            previousLocalPos = transform.localPosition;
            transform.localPosition = new Vector3(transform.localPosition.x + 2 * 6.048f, transform.localPosition.y);
        }

        if (PlayerManager.player.transform.position.x - transform.position.x < -27.5f)
        {
            transform.localPosition = previousLocalPos;
        }
    }
}
