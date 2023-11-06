using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float cameraSpeed;
    float xDifference = 6.5f, yDiffrence = 5.4f;

    Vector3 endPos;

    void Update()
    {
        endPos = new Vector3(player.position.x + xDifference, yDiffrence, -10);

        transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime * cameraSpeed);
    }
}
// resi collider da mu se smanjuje