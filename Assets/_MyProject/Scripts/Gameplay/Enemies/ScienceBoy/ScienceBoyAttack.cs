using System.Collections;
using UnityEngine;

public class ScienceBoyAttack : MonoBehaviour
{

    private IEnumerator Start()
    {
        EnemySO _enemySO = EnemySO.Get(2);
        yield return null;
        GameplayManager.Instance.DamageHouse(_enemySO.AttackDamage);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
