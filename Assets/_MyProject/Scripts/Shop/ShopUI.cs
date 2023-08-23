using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [SerializeField] Button homeButton;
    [SerializeField] Transform itemsHolder;
    [SerializeField] WeaponUpgrade weaponUpgradePrefab;
    [SerializeField] AbilityUnlock abilityUnlockPrefab;
    [SerializeField] HealthUpgrade healthUpgradePrefab;
    [SerializeField] Button shownNextButton;
    [SerializeField] Button showPreviusButton;
    [SerializeField] TextMeshProUGUI goldDisplay;

    List<GameObject> shownItems = new List<GameObject>();

    private void OnEnable()
    {
        homeButton.onClick.AddListener(GoHome);
        PlayerData.UpdatedGold += ShowGold;
    }

    private void OnDisable()
    {
        homeButton.onClick.RemoveListener(GoHome);
        PlayerData.UpdatedGold -= ShowGold;
    }

    private void Start()
    {
        LoadWeapons();
        ShowGold();
    }

    void LoadWeapons()
    {
        ClearItems();
        shownNextButton.onClick.AddListener(ShowAbilities);
        showPreviusButton.onClick.AddListener(ShowHealthUpgrade);
        List<GunSO> _guns = GunSO.Get().ToList();
        _guns = _guns.OrderBy(element => element.Id).ToList();

        foreach (var _weapon in _guns)
        {
            WeaponUpgrade _weaponDisplay = Instantiate(weaponUpgradePrefab, itemsHolder);
            bool _found = false;
            foreach (var _unlockedWeapon in DataManager.Instance.PlayerData.UnlockedWeapons)
            {
                if (_unlockedWeapon.Id == _weapon.Id)
                {
                    _found = true;
                    break;
                }
            }

            if (_found)
            {
                _weaponDisplay.SetupUnlocked(_weapon);
            }
            else
            {
                _weaponDisplay.SetupLocked(_weapon);
            }
            shownItems.Add(_weaponDisplay.gameObject);
        }
    }

    void ShowAbilities()
    {
        ClearItems();
        shownNextButton.onClick.AddListener(ShowHealthUpgrade);
        showPreviusButton.onClick.AddListener(LoadWeapons);
        List<AbilitiesSO> _abilities = AbilitiesSO.Get().ToList();
        _abilities = _abilities.OrderBy(element => element.Id).ToList();

        foreach (var _ability in _abilities)
        {
            AbilityUnlock _abilityDisplay = Instantiate(abilityUnlockPrefab, itemsHolder);
            bool _found = false;
            foreach (var _unlockedAbility in DataManager.Instance.PlayerData.UnlockedAbilities)
            {
                if (_unlockedAbility == _ability.Id)
                {
                    _found = true;
                    break;
                }
            }

            if (_found)
            {
                _abilityDisplay.SetUnlocked(_ability);
            }
            else
            {
                _abilityDisplay.SetLocked(_ability);
            }
            shownItems.Add(_abilityDisplay.gameObject);
        }
    }

    void ShowHealthUpgrade()
    {
        ClearItems();
        shownNextButton.onClick.AddListener(LoadWeapons);
        showPreviusButton.onClick.AddListener(ShowAbilities);
        HealthUpgrade _healthDisplay = Instantiate(healthUpgradePrefab, itemsHolder);
        _healthDisplay.Setup();
        shownItems.Add(_healthDisplay.gameObject);
    }

    void GoHome()
    {
        SceneManager.LoadMainMenu();
    }

    void ClearItems()
    {
        shownNextButton.onClick.RemoveAllListeners();
        showPreviusButton.onClick.RemoveAllListeners();
        foreach (var _item in shownItems)
        {
            Destroy(_item);
        }

        shownItems.Clear();
    }

    void ShowGold()
    {
        goldDisplay.text = DataManager.Instance.PlayerData.Gold.ToString();
    }
}
