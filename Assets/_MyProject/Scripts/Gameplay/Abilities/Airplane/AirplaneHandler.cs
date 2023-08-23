using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AirplaneHandler : MonoBehaviour
{
    public static float SpeedMultiplayer = 1;

    [SerializeField] Button handleClick;
    [SerializeField] float cooldown;
    [SerializeField] GameObject cooldwonHolder;
    [SerializeField] TextMeshProUGUI cooldownDisplay;
    [SerializeField] AirplaneObject airplaneObject;

    float cooldownCounter;
    bool isOnCooldwon = false;

    private void OnEnable()
    {
        handleClick.onClick.AddListener(HandleClick);
        if (!DataManager.Instance.PlayerData.UnlockedAbilities.Contains(3))
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
        cooldwonHolder.SetActive(true);
        isOnCooldwon = true;
        cooldownCounter = cooldown;
        airplaneObject.StartFlying();
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
    }
}
