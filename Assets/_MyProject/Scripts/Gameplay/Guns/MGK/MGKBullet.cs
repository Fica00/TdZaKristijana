using UnityEngine;

public class MGKBullet : BulletController
{
    [SerializeField] GameObject effect;
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
            _enemyObject.TakeDamage(damage);
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