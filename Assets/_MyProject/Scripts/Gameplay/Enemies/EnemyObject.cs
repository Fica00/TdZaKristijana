using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyObject : MonoBehaviour
{
    public static Action<EnemyObject> Died;
    [SerializeField] protected EnemySO enemySO;
    [SerializeField] protected Image fillBar;

    protected SpriteRenderer spriteRenderer;
    protected float health;
    protected float maxHealth;
    protected bool canAttack = true;
    protected bool isDead = false;

    public float Health => health;
    public EnemySO EnemySO => enemySO;

    public virtual void Setup(int _sortingLayer)
    {
        throw new Exception("Setup must be overriden on: " + gameObject.name);
    }

    public virtual void Scale()
    {
        health += (LevelManager.Instance.SelectedLevel.Id * 0.05f * health);
        maxHealth = health;
    }

    protected virtual void Attack()
    {
        throw new Exception("Attack must be overriden on: " + gameObject.name);
    }

    public virtual void TakeDamage(float _damage)
    {
        throw new Exception("Take damage must be overriden on: " + gameObject.name);
    }

    protected virtual void Die()
    {
        throw new Exception("Die must be overriden");
    }

    protected void FlipSprites(bool flipDir)
    {
        int direction = flipDir ? 180 : 0;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, direction, transform.eulerAngles.z);
    }
}
