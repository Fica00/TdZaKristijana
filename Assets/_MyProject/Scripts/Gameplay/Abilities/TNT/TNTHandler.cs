using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TNTHandler : MonoBehaviour
{
    [SerializeField] Button handleClick;
    [SerializeField] float cooldown;
    [SerializeField] GameObject cooldwonHolder;
    [SerializeField] TextMeshProUGUI cooldownDisplay;
    [SerializeField] Transform tntSpawnPoint;
    [SerializeField] TNTObject tntPrefab;
    [SerializeField] List<Vector2> velocityOfTNTs;

    float cooldownCounter;
    bool isOnCooldwon = false;

    private void OnEnable()
    {
        handleClick.onClick.AddListener(HandleClick);
        if (!DataManager.Instance.PlayerData.UnlockedAbilities.Contains(2))
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
        foreach (var _velocity in velocityOfTNTs)
        {
            TNTObject _tnt = Instantiate(tntPrefab);
            tntPrefab.transform.position = tntSpawnPoint.position;

            _tnt.GetComponent<Rigidbody2D>().velocity = _velocity;
        }
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
