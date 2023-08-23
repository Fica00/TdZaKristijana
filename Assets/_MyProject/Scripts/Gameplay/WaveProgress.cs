using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveProgress : MonoBehaviour
{
    [SerializeField] Slider slider;
    public static Action Won;

    float enemiesKilled = 0;
    float enemiesToKill;

    private void OnEnable()
    {
        EnemyObject.Died += IncreaseCount;
    }

    private void OnDisable()
    {
        EnemyObject.Died -= IncreaseCount;
    }

    private void IncreaseCount(EnemyObject _enemyObject)
    {
        enemiesKilled++;
        slider.value = 1 - (enemiesKilled / enemiesToKill);
        if (enemiesKilled == enemiesToKill)
        {
            StartCoroutine(TriggerWon());
        }
    }

    IEnumerator TriggerWon()
    {
        yield return new WaitForSeconds(0.2f);
        Won?.Invoke();
    }

    public void Setup(LevelData _selectedLevel)
    {
        enemiesToKill = _selectedLevel.EnemySpawns.Count;
    }
}
