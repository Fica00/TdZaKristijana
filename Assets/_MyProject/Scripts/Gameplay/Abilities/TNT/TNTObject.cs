using UnityEngine;

public class TNTObject : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float damage;
    [SerializeField] GameObject explosionEffect;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TNTGround")
        {
            ExplodeDamage();
            Rigidbody2D _rigidBody = gameObject.GetComponent<Rigidbody2D>();
            _rigidBody.gravityScale = 0;
            _rigidBody.velocity = Vector2.zero;

            GameObject _explosionEffect = Instantiate(explosionEffect);
            _explosionEffect.transform.position = transform.position;

            Destroy(gameObject);
            AudioManager.Instance.PlaySoundEffect(AudioManager.BOMB_EXPLOSION);
        }
    }

    void ExplodeDamage()
    {
        EnemyObject[] _enemies = FindObjectsOfType<EnemyObject>();
        foreach (var _enemy in _enemies)
        {
            if (Vector2.Distance(transform.position, _enemy.transform.position) < range)
            {
                _enemy.TakeDamage(damage);
            }
        }
    }
}
