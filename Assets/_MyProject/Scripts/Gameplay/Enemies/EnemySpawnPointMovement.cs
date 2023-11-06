using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPointMovement : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(PlayerManager.player.transform.position.x + 17, 5.6f, 0);
    }
}

