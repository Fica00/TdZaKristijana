using System.Collections;
using UnityEngine;

public class BulkManAttack : MonoBehaviour
{
    private IEnumerator Start()
    {
        EnemySO _enemySO = EnemySO.Get(3);
        yield return null;
        float _moveDistance = 3;
        transform.position += (Vector3.left * _moveDistance);
        GameplayManager.Instance.DamageHouse(_enemySO.AttackDamage);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
