using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class BaseHealthHandler : MonoBehaviour
{
    public static Action ILost;
    [SerializeField] Image fillProgress;
    [SerializeField] TextMeshProUGUI healthDisplay;

    float startingHealth;
    float currentHealth;
    public bool isDead;

    public void Setup(float _startingHealth)
    {
        startingHealth = _startingHealth;
        currentHealth = startingHealth;
        DisplayHealth();
        GameplayManager.TakeDamage += TakeDamage;
        LosePanel.PlayerReviewd += Review;
    }

    private void OnDisable()
    {
        GameplayManager.TakeDamage -= TakeDamage;
        LosePanel.PlayerReviewd -= Review;
    }

    void TakeDamage(float _damage)
    {
        if (isDead)
        {
            return;
        }
        currentHealth -= _damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        DisplayHealth();
        if (currentHealth == 0)
        {
            isDead = true;
            ILost?.Invoke();
        }
        AudioManager.Instance.PlayRandomHitNoise();
    }

    void Review()
    {
        StartCoroutine(ReviewRoutine());
    }

    IEnumerator ReviewRoutine()
    {
        currentHealth = (startingHealth * 20) / 100;
        TakeDamage(0);
        yield return new WaitForSeconds(2); //invisibility
        isDead = false;
    }

    void DisplayHealth()
    {
        healthDisplay.text = currentHealth + "/" + startingHealth;
        fillProgress.fillAmount = currentHealth / startingHealth;
    }
}
