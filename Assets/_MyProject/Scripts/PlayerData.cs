using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public const string HOUSE_LEVLE_KEY = "HouseLevel";
    public const string UNLOCKED_LEVEL_KEY = "UnlockedLevel";
    public const string SELECTED_GUNS_KEY = "SelectedGuns";
    public const string UNLOCKED_Weapons_KEY = "UnlockedWeapons";
    public const string GOLD_KEY = "Gold";
    public const string UNLOCKED_ABILITIES_KEY = "UnlockedAbilities";

    int houseLevel;
    bool playMusic = true;
    bool playSoundEffect = true;
    int unlockedLevel;
    int gold;
    List<Weapon> unlockedWeapons;
    List<int> unlockedAbilities;

    //selected gun -1 means that that slot is empty
    List<int> selectedGuns;


    public static Action UpdatedHouseLevel;
    public static Action UpdatedMusic;
    public static Action UpdatedSoundEffects;
    public static Action UpdatedUnlockedLevel;
    public static Action UpdatedSelectedGuns;
    public static Action UpdatedGold;
    public static Action UpdatedUnlockedGuns;
    public static Action UpdatedUnlockedAbilities;

    public PlayerData()
    {
        if (PlayerPrefs.HasKey(HOUSE_LEVLE_KEY))
        {
            houseLevel = PlayerPrefs.GetInt(HOUSE_LEVLE_KEY);
        }
        else
        {
            houseLevel = 0;
        }

        if (PlayerPrefs.HasKey(UNLOCKED_LEVEL_KEY))
        {
            unlockedLevel = PlayerPrefs.GetInt(UNLOCKED_LEVEL_KEY);
        }
        else
        {
            unlockedLevel = 1;
        }

        if (PlayerPrefs.HasKey(SELECTED_GUNS_KEY))
        {
            selectedGuns = JsonConvert.DeserializeObject<List<int>>(PlayerPrefs.GetString(SELECTED_GUNS_KEY));
        }
        else
        {
            selectedGuns = new List<int>() { 0, -1, -1 };
            PlayerPrefs.SetString(SELECTED_GUNS_KEY, JsonConvert.SerializeObject(selectedGuns));
        }

        if (PlayerPrefs.HasKey(UNLOCKED_Weapons_KEY))
        {
            unlockedWeapons = JsonConvert.DeserializeObject<List<Weapon>>(PlayerPrefs.GetString(UNLOCKED_Weapons_KEY));
        }
        else
        {
            unlockedWeapons = new List<Weapon>() { new Weapon() { Id = 0, Upgrade1 = 0, Upgrade2 = 0 } };
            PlayerPrefs.SetString(UNLOCKED_Weapons_KEY, JsonConvert.SerializeObject(unlockedWeapons));
        }

        if (PlayerPrefs.HasKey(GOLD_KEY))
        {
            gold = PlayerPrefs.GetInt(GOLD_KEY);
        }
        else
        {
            PlayerPrefs.SetInt(GOLD_KEY, 0);
            gold = 0;
        }

        if (PlayerPrefs.HasKey(UNLOCKED_ABILITIES_KEY))
        {
            unlockedAbilities = JsonConvert.DeserializeObject<List<int>>(PlayerPrefs.GetString(UNLOCKED_ABILITIES_KEY));
        }
        else
        {
            unlockedAbilities = new List<int>();
            PlayerPrefs.SetString(UNLOCKED_ABILITIES_KEY, JsonConvert.SerializeObject(unlockedAbilities));
        }
    }

    public int HouseLevel
    {
        get
        {
            return houseLevel;
        }
        set
        {
            houseLevel = value;
            UpdatedHouseLevel?.Invoke();
        }
    }

    public bool PlayMusic
    {
        get
        {
            return playMusic;
        }
        set
        {
            playMusic = value;
            UpdatedMusic?.Invoke();
        }
    }

    public bool PlaySoundEffect
    {
        get
        {
            return playSoundEffect;
        }
        set
        {
            playSoundEffect = value;
            UpdatedSoundEffects?.Invoke();
        }
    }

    public int UnlockedLevel
    {
        get
        {
            return unlockedLevel;
        }
        set
        {
            unlockedLevel = value;
            UpdatedUnlockedLevel?.Invoke();
        }
    }

    public List<int> SelectedGuns
    {
        get
        {
            return selectedGuns;
        }
    }

    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            UpdatedGold?.Invoke();
        }
    }

    public int GetUpgrade1Level(int _weaponId)
    {
        foreach (var _unlockedWeapon in unlockedWeapons)
        {
            if (_unlockedWeapon.Id == _weaponId)
            {
                return _unlockedWeapon.Upgrade1;
            }
        }
        throw new Exception("didnt find upgrade1 level for weapon: " + _weaponId);
    }

    public int GetUpgrade2Level(int _weaponId)
    {
        foreach (var _unlockedWeapon in unlockedWeapons)
        {
            if (_unlockedWeapon.Id == _weaponId)
            {
                return _unlockedWeapon.Upgrade2;
            }
        }
        throw new Exception("didnt find upgrade1 level for weapon: " + _weaponId);
    }

    public List<Weapon> UnlockedWeapons
    {
        get
        {
            return unlockedWeapons;
        }
    }

    public void UnlockWeapon(int _weaponId)
    {
        unlockedWeapons.Add(new Weapon() { Id = _weaponId, Upgrade1 = 0, Upgrade2 = 0 });
        UpdatedUnlockedGuns?.Invoke();
    }

    public void SelectWeapon(int _slotId, int _weaponId)
    {
        selectedGuns[_slotId] = _weaponId;
        UpdatedSelectedGuns?.Invoke();
    }

    public void UpgradeWeapon(int _weaponId, int _upgrade)
    {
        if (_upgrade == 1)
        {
            unlockedWeapons.Find(element => element.Id == _weaponId).Upgrade1++;
        }
        else
        {
            unlockedWeapons.Find(element => element.Id == _weaponId).Upgrade2++;
        }
        UpdatedUnlockedGuns?.Invoke();
    }

    public List<int> UnlockedAbilities
    {
        get
        {
            return unlockedAbilities;
        }
    }

    public void UnlockAbility(int _id)
    {
        unlockedAbilities.Add(_id);
        UpdatedUnlockedAbilities?.Invoke();
    }
}
