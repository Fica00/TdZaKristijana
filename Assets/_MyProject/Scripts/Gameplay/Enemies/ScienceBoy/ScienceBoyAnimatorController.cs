using UnityEngine;

public class ScienceBoyAnimatorController : MonoBehaviour
{
    [SerializeField] ScienceBoyController controller;

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
