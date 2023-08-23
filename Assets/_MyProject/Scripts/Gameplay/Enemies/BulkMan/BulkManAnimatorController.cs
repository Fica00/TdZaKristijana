using UnityEngine;

public class BulkManAnimatorController : MonoBehaviour
{
    [SerializeField] BulkManController controller;

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
