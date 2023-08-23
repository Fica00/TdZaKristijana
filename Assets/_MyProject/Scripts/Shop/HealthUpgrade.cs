using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class HealthUpgrade : MonoBehaviour
{
    [SerializeField] Button upgradeButton;
    [SerializeField] TextMeshProUGUI upgradeCostDisplay;
    [SerializeField] GameObject[] upgradeDisplays;

    [SerializeField] List<int> upgradeCosts;

    int upgradeCost;

    private void OnEnable()
    {
        upgradeButton.onClick.AddListener(Upgrade);
    }

    private void OnDisable()
    {
        upgradeButton.onClick.RemoveListener(Upgrade);
    }

    void Upgrade()
    {
        int upgradeCost = upgradeCosts[DataManager.Instance.PlayerData.HouseLevel];
        if (DataManager.Instance.PlayerData.Gold >= upgradeCost)
        {
            DataManager.Instance.PlayerData.Gold -= (int)upgradeCost;
            DataManager.Instance.PlayerData.HouseLevel++;
            Setup();
        }
        else
        {
            UIManager.Instance.OkDialog.Show("You don't have enaught gold");
        }
    }

    public void Setup()
    {
        if (DataManager.Instance.PlayerData.HouseLevel < upgradeCosts.Count)
        {
            upgradeCost = upgradeCosts[DataManager.Instance.PlayerData.HouseLevel];
            upgradeCostDisplay.text = upgradeCost.ToString();
        }
        else
        {
            upgradeCostDisplay.text = "MAX";
            upgradeButton.interactable = false;
        }

        for (int i = 0; i < DataManager.Instance.PlayerData.HouseLevel; i++)
        {
            upgradeDisplays[i].SetActive(true);
        }
    }
}
