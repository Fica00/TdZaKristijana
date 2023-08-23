using UnityEngine;

public class CatAnimatorTrigger : MonoBehaviour
{
    [SerializeField] CatController controller;

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
