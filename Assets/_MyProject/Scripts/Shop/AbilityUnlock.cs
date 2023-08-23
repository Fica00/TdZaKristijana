using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilityUnlock : MonoBehaviour
{
    [SerializeField] Image iconDisplay;
    [SerializeField] GameObject unlockedHolder;
    [SerializeField] GameObject lockedHolder;
    [SerializeField] Button unlockButton;
    [SerializeField] TextMeshProUGUI unlockPrice;

    AbilitiesSO abilitiSO;
    float price;

    private void OnEnable()
    {
        unlockButton.onClick.AddListener(Unlock);
    }

    private void OnDisable()
    {
        unlockButton.onClick.RemoveListener(Unlock);
    }

    void Unlock()
    {
        if (DataManager.Instance.PlayerData.Gold >= price)
        {
            DataManager.Instance.PlayerData.Gold -= (int)price;
            DataManager.Instance.PlayerData.UnlockAbility(abilitiSO.Id);
            SetUnlocked(abilitiSO);
        }
        else
        {
            UIManager.Instance.OkDialog.Show("You dont have enaught gold");
        }
    }

    public void SetUnlocked(AbilitiesSO _abilitySO)
    {
        abilitiSO = _abilitySO;
        iconDisplay.sprite = abilitiSO.Sprite;

        unlockedHolder.SetActive(true);
        lockedHolder.SetActive(false);
    }

    public void SetLocked(AbilitiesSO _abilitySO)
    {
        abilitiSO = _abilitySO;
        iconDisplay.sprite = abilitiSO.Sprite;

        unlockedHolder.SetActive(false);
        lockedHolder.SetActive(true);

        price = _abilitySO.Cost;
        unlockPrice.text = price.ToString();
    }
}
