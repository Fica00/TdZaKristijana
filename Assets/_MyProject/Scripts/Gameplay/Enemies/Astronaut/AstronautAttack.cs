using System.Collections;
using UnityEngine;

public class AstronautAttack : MonoBehaviour
{
    private IEnumerator Start()
    {
        EnemySO _enemySO = EnemySO.Get(0);
        yield return null;
        float _moveDistance = 3;
        transform.position += (Vector3.left * _moveDistance);
        GameplayManager.Instance.DamageHouse(_enemySO.AttackDamage);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
