using Newtonsoft.Json;
using System;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    PlayerData playerData;
    public PlayerData PlayerData => playerData;

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    void UnsubscribeEvents()
    {
        PlayerData.UpdatedHouseLevel -= SaveHauseLevel;
        PlayerData.UpdatedUnlockedLevel -= SaveUnlockedLevel;
        PlayerData.UpdatedSelectedGuns -= SaveSelectedGuns;
        PlayerData.UpdatedGold -= SaveGold;
        PlayerData.UpdatedUnlockedGuns -= SaveUnlockedGuns;
        PlayerData.UpdatedUnlockedAbilities -= SaveAbilities;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            playerData = new PlayerData();
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SubscribeEvents();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SubscribeEvents()
    {
        PlayerData.UpdatedHouseLevel += SaveHauseLevel;
        PlayerData.UpdatedUnlockedLevel += SaveUnlockedLevel;
        PlayerData.UpdatedSelectedGuns += SaveSelectedGuns;
        PlayerData.UpdatedGold += SaveGold;
        PlayerData.UpdatedUnlockedGuns += SaveUnlockedGuns;
        PlayerData.UpdatedUnlockedAbilities += SaveAbilities;
    }

    private void SaveHauseLevel()
    {
        PlayerPrefs.SetInt(PlayerData.HOUSE_LEVLE_KEY, playerData.HouseLevel);
    }

    private void SaveUnlockedLevel()
    {
        PlayerPrefs.SetInt(PlayerData.UNLOCKED_LEVEL_KEY, playerData.UnlockedLevel);
    }

    private void SaveSelectedGuns()
    {
        PlayerPrefs.SetString(PlayerData.SELECTED_GUNS_KEY, JsonConvert.SerializeObject(playerData.SelectedGuns));
    }

    private void SaveGold()
    {
        PlayerPrefs.SetInt(PlayerData.GOLD_KEY, playerData.Gold);
    }

    private void SaveUnlockedGuns()
    {
        PlayerPrefs.SetString(PlayerData.UNLOCKED_Weapons_KEY, JsonConvert.SerializeObject(playerData.UnlockedWeapons));
    }

    private void SaveAbilities()
    {
        PlayerPrefs.SetString(PlayerData.UNLOCKED_ABILITIES_KEY, JsonConvert.SerializeObject(playerData.UnlockedAbilities));
    }
}
