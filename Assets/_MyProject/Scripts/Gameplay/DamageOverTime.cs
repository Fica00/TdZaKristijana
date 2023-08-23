using System.Collections;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    EnemyObject enemy;
    float damageAmount;
    float duration;
    GameObject visual;
    float tickCooldown;

    float tickCounter;
    float totalTimeCounter;

    public void Setup(float _damageAmount, float _duration, float _tickCooldown, GameObject _visual)
    {
        damageAmount = _damageAmount;
        duration = _duration;
        tickCooldown = _tickCooldown;
        enemy = GetComponentInParent<EnemyObject>();
        visual = _visual;
    }

    private void Start()
    {
        StartCoroutine(TakeDamageRoutine());
    }

    IEnumerator TakeDamageRoutine()
    {
        while (true)
        {
            if (tickCounter >= tickCooldown)
            {
                tickCounter = 0;
                enemy.TakeDamage(damageAmount);
            }
            if (totalTimeCounter >= duration)
            {
                break;
            }
            tickCounter += Time.deltaTime;
            totalTimeCounter += Time.deltaTime;
            yield return null;
        }
        Destroy(visual);
        Destroy(this);
    }
}
