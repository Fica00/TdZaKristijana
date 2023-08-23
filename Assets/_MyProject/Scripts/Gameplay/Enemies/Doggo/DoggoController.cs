using System.Collections;
using UnityEngine;
using DG.Tweening;

public class DoggoController : EnemyObject
{
    [SerializeField] Animator animatorController;
    [SerializeField] Transform shootPoint;
    [SerializeField] SpriteRenderer graphics;
    [SerializeField] GameObject fillBarHolder;
    Transform target;
    Transform myTransform;

    bool isAttacking = false;
    bool isIdle = false;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("TargetForEnemies").transform;
        myTransform = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        if (isDead || isAttacking)
        {
            return;
        }

        Move();
        TryToAttack();
        CheckForIdle();
    }

    void Move()
    {
        Vector3 _targetPosition = target.position;
        _targetPosition.y = myTransform.position.y;
        float _distance = Vector3.Distance(myTransform.position, _targetPosition);
        if (_distance > enemySO.Range)
        {
            myTransform.position = Vector3.MoveTowards(
                 myTransform.position,
                 _targetPosition, enemySO.MovementSpeed * Time.deltaTime * FridgeHandler.SpeedMultiplayer);
        }
    }

    void TryToAttack()
    {
        Vector3 _targetPosition = target.position;
        _targetPosition.y = myTransform.position.y;
        float _distance = Vector3.Distance(myTransform.position, _targetPosition);
        if (_distance <= enemySO.Range)
        {
            Attack();
        }
    }

    void CheckForIdle()
    {
        Vector3 _targetPosition = target.position;
        _targetPosition.y = myTransform.position.y;
        float _distance = Vector3.Distance(myTransform.position, _targetPosition);
        if (_distance <= enemySO.Range && !canAttack && !isAttacking)
        {
            Idle();
        }
    }

    void Idle()
    {
        if (isIdle)
        {
            return;
        }

        isIdle = true;
        animatorController.Play("Idle");
    }

    public override void Setup(int _sortingLayer)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = enemySO.Health;
        graphics.sortingOrder = _sortingLayer;
    }

    protected override void Attack()
    {
        if (!canAttack)
        {
            return;
        }
        isAttacking = true;
        animatorController.Play("Attack");
        StartCoroutine(AttackCooldown());
    }

    public void SpawnAttackObject()
    {
        GameObject _attack = Instantiate(enemySO.AttackPrefab);
        _attack.transform.position = shootPoint.transform.position;
        _attack.GetComponent<DoggoAttack>().Setup(target, enemySO);
    }

    public override void TakeDamage(float _damage)
    {
        if (isDead)
        {
            fillBarHolder.SetActive(false);
            return;
        }
        health -= _damage;
        if (health <= 0)
        {
            if (isDead)
            {
                return;
            }
            isDead = true;
            Die();
        }
        else
        {
            fillBarHolder.SetActive(true);
            DOTween.To(() => fillBar.fillAmount, x => fillBar.fillAmount = x, health / maxHealth, 1.0f).onComplete += () => { fillBarHolder.SetActive(false); };
        }

        AudioManager.Instance.PlayRandomHitNoise();
    }

    protected override void Die()
    {
        fillBar.fillAmount = 0;
        isDead = true;
        Destroy(GetComponent<Collider2D>());
        animatorController.Play("Died");
        Died?.Invoke(this);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(enemySO.AttackCooldown);
        canAttack = true;
    }

    public void FinishedAttack()
    {
        isAttacking = false;
        animatorController.Play("Idle");
    }
}
