using UnityEngine;

public class RobotAnimatorController : MonoBehaviour
{
    [SerializeField] RobotController controller;

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
