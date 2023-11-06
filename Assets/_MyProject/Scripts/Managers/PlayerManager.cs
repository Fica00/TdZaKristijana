using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static GameObject player;

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

}
