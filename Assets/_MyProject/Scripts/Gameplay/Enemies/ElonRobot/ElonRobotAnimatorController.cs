using UnityEngine;

public class ElonRobotAnimatorController : MonoBehaviour
{
    [SerializeField] ElonRobotController controller;

    //all methods are called from animation triggers
    void FinishedAttack()
    {
        controller.FinishedAttack();
    }

    void SpawnAttackObject()
    {
        controller.SpawnAttackObject();
    }

    void DestroyObject()
    {
        controller.DestroyObject();
    }
}
