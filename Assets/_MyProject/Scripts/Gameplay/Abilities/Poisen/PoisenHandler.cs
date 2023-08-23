using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class PoisenHandler : MonoBehaviour
{
    [SerializeField] Button handleClick;
    [SerializeField] float tickCooldown;
    [SerializeField] float tickDamage;
    [SerializeField] float cooldown;
    [SerializeField] float duration;
    [SerializeField] GameObject cooldwonHolder;
    [SerializeField] TextMeshProUGUI cooldownDisplay;
    [SerializeField] GameObject screenBlur;

    float cooldownCounter;
    float durationCounter;
    bool isOnCooldwon = false;
    bool isActive = false;
    float tickCounter;
    private void OnEnable()
    {
        handleClick.onClick.AddListener(HandleClick);
        if (!DataManager.Instance.PlayerData.UnlockedAbilities.Contains(1))
        {
            handleClick.interactable = false;
            cooldwonHolder.SetActive(true);
            cooldownDisplay.text = string.Empty;
        }
    }

    private void OnDisable()
    {
        handleClick.onClick.RemoveListener(HandleClick);
    }

    void HandleClick()
    {
        if (cooldownCounter <= 0)
        {
            Activate();
        }
    }

    void Activate()
    {
        AudioManager.Instance.PlaySoundEffect(AudioManager.RAIN);
        isActive = true;
        cooldwonHolder.SetActive(true);
        screenBlur.SetActive(true);
        isOnCooldwon = true;
        cooldownCounter = cooldown;
        durationCounter = duration;
        tickCounter = tickCooldown;
    }

    private void FixedUpdate()
    {
        if (cooldownCounter > 0)
        {
            cooldownCounter -= Time.deltaTime;
            cooldownDisplay.text = Mathf.RoundToInt(cooldownCounter).ToString();
        }
        else
        {
            if (isOnCooldwon)
            {
                isOnCooldwon = false;
                cooldwonHolder.SetActive(false);
            }
        }

        if (durationCounter > 0)
        {
            durationCounter -= Time.deltaTime;
        }
        else
        {
            if (isActive)
            {
                isActive = false;
                screenBlur.SetActive(false);
            }
        }

        if (isActive)
        {
            tickCounter -= Time.deltaTime;
            if (tickCounter <= 0)
            {
                tickCounter = tickCooldown;
                List<EnemyObject> _enemies = FindObjectsOfType<EnemyObject>().ToList();
                foreach (var _enemy in _enemies)
                {
                    _enemy.TakeDamage(tickDamage);
                }
            }
        }
    }
}
