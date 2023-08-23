using UnityEngine;

public class RPGBullet : BulletController
{
    [SerializeField] GameObject effect;
    [SerializeField] float attackRange;

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

            Collider2D[] _hitColliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(attackRange, attackRange), 0f);
            for (int i = 0; i < _hitColliders.Length; i++)
            {
                EnemyObject enemyObject = _hitColliders[i].GetComponent<EnemyObject>();
                if (enemyObject != null && enemyObject.Health > 0)
                {
                    enemyObject.TakeDamage(damage);
                }
            }

            if (effect != null)
            {
                Instantiate(effect).transform.position = transform.position;
            }
            Destroy(gameObject);
        }

        if (collision.tag == "Boundries")
        {
            Destroy(gameObject);
        }
    }
}