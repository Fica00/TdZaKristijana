using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FridgeHandler : MonoBehaviour
{
    public static float SpeedMultiplier = 1;

    [SerializeField] Button handleClick;
    [Range(0f, 0.9f)] [SerializeField] float effect;
    [SerializeField] float cooldown;
    [SerializeField] float duration;
    [SerializeField] GameObject cooldwonHolder;
    [SerializeField] TextMeshProUGUI cooldownDisplay;
    [SerializeField] GameObject screenBlur;

    float cooldownCounter;
    float durationCounter;
    bool isOnCooldwon = false;
    bool isActive = false;

    private void OnEnable()
    {
        handleClick.onClick.AddListener(HandleClick);
        if (!DataManager.Instance.PlayerData.UnlockedAbilities.Contains(0))
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
        isActive = true;
        cooldwonHolder.SetActive(true);
        screenBlur.SetActive(true);
        isOnCooldwon = true;
        SpeedMultiplier = effect;
        cooldownCounter = cooldown;
        durationCounter = duration;
        AudioManager.Instance.PlaySoundEffect(AudioManager.FREEZE);
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
                SpeedMultiplier = 1;
                screenBlur.SetActive(false);
            }
        }
    }
}
