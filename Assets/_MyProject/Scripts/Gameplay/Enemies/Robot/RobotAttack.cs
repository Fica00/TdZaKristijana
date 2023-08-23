using UnityEngine;

public class RobotAttack : MonoBehaviour
{
    float damage;
    float speed;
    Transform target;
    Transform myTransform;

    public void Setup(Transform _target, EnemySO _enemySO)
    {
        damage = _enemySO.AttackDamage;
        speed = _enemySO.BulletSpeed;
        myTransform = transform;
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        Move();
    }

    void Move()
    {
        Vector3 _targetPosition = target.position;
        _targetPosition.y = myTransform.position.y;
        float _distance = Vector3.Distance(myTransform.position, _targetPosition);
        if (_distance > 0.1f)
        {
            myTransform.position = Vector3.MoveTowards(
                 myTransform.position,
                 _targetPosition, speed * Time.deltaTime * FridgeHandler.SpeedMultiplayer);
        }
        else
        {
            target = null;
            Damage();
        }
    }

    void Damage()
    {
        GameplayManager.Instance.DamageHouse(damage);
        Destroy(gameObject);
    }
}
