using UnityEngine;

public class FlameTrowerBullet : BulletController
{
    [SerializeField] float duration;
    [SerializeField] float tickCooldown;
    [SerializeField] GameObject visual;

    public override void SetDamage(float _damage)
    {
        damage = _damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyObject _enemyObject = collision.GetComponent<EnemyObject>();
        if (_enemyObject != null)
        {
            if (_enemyObject.Health <= 0)
            {
                return;
            }
            if (_enemyObject.GetComponent<DamageOverTime>() != null)
            {
                return;
            }
            DamageOverTime _damageOverTime = _enemyObject.gameObject.AddComponent<DamageOverTime>();
            GameObject _visuals = Instantiate(visual, _enemyObject.transform);
            _visuals.transform.localPosition = Vector3.zero;
            _damageOverTime.Setup(damage, duration, tickCooldown, _visuals);
        }

        if (collision.tag == "Boundries")
        {
            Destroy(gameObject);
        }
    }
}