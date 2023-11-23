using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static GameObject player;

    [SerializeField] BaseHealthHandler healthHandler;
    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

}
