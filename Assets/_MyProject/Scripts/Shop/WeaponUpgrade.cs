using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WeaponUpgrade : MonoBehaviour
{

    public static Action<GunSO> Equipt;

    [SerializeField] GameObject unlockedHolder;
    [SerializeField] GameObject lockedHolder;
    [SerializeField] Image iconDisplat;
    [SerializeField] TextMeshProUGUI unlockPrice;
    [SerializeField] Button unlockButton;

    [SerializeField] TextMeshProUGUI nameDisplay;
    [SerializeField] Button upgrade1Button;
    [SerializeField] Button upgrade2Button;
    [SerializeField] TextMeshProUGUI upgrade1CostDisplay;
    [SerializeField] TextMeshProUGUI upgrade2CostDisplay;
    [SerializeField] GameObject[] upgradeDisplays;
    [SerializeField] Button equiptButotn;

    GunSO weapon;
    Weapon weaponData;
    float upgrade1Cost;
    float upgrade2Cost;

    private void OnEnable()
    {
        unlockButton.onClick.AddListener(Unlock);
        upgrade1Button.onClick.AddListener(Upgrade1);
        upgrade2Button.onClick.AddListener(Upgrade2);
        equiptButotn.onClick.AddListener(TriggerEquipt);
    }

    private void OnDisable()
    {
        unlockButton.onClick.AddListener(Unlock);
        upgrade1Button.onClick.RemoveListener(Upgrade1);
        upgrade2Button.onClick.RemoveListener(Upgrade2);
        equiptButotn.onClick.RemoveListener(TriggerEquipt);
    }

    void Unlock()
    {
        if (weapon.UnclockCost <= DataManager.Instance.PlayerData.Gold)
        {
            DataManager.Instance.PlayerData.Gold -= weapon.UnclockCost;
            DataManager.Instance.PlayerData.UnlockWeapon(weapon.Id);
            SetupUnlocked(weapon);
        }
        else
        {
            UIManager.Instance.OkDialog.Show("You dont have enaught gold");
        }
    }

    void Upgrade1()
    {
        if (DataManager.Instance.PlayerData.Gold >= upgrade1Cost)
        {
            DataManager.Instance.PlayerData.Gold -= (int)upgrade1Cost;
            DataManager.Instance.PlayerData.UpgradeWeapon(weapon.Id, 1);
            SetupUnlocked(weapon);
        }
        else
        {
            UIManager.Instance.OkDialog.Show("You don't have enaught gold");
        }
    }

    void Upgrade2()
    {
        if (DataManager.Instance.PlayerData.Gold >= upgrade2Cost)
        {
            DataManager.Instance.PlayerData.Gold -= (int)upgrade2Cost;
            DataManager.Instance.PlayerData.UpgradeWeapon(weapon.Id, 2);
            SetupUnlocked(weapon);
        }
        else
        {
            UIManager.Instance.OkDialog.Show("You don't have enaught gold");
        }
    }

    void TriggerEquipt()
    {
        Equipt?.Invoke(weapon);
    }

    public void SetupLocked(GunSO _gun)
    {
        weapon = _gun;
        iconDisplat.sprite = _gun.Sprite;
        unlockPrice.text = weapon.UnclockCost.ToString();

        unlockedHolder.SetActive(false);
        lockedHolder.SetActive(true);
    }

    public void SetupUnlocked(GunSO _gun)
    {
        weapon = _gun;
        iconDisplat.sprite = _gun.Sprite;
        nameDisplay.text = _gun.Name;

        weaponData = DataManager.Instance.PlayerData.UnlockedWeapons.Find(element => element.Id == _gun.Id);

        if (weapon.Upgrade1Cost.Count == weaponData.Upgrade1)
        {
            upgrade1Button.interactable = false;
            upgrade1CostDisplay.text = "MAX";
        }
        else
        {
            upgrade1Cost = weapon.Upgrade1Cost[weaponData.Upgrade1];
            upgrade1CostDisplay.text = upgrade1Cost.ToString();
        }
        if (weapon.Upgrade2Cost.Count == weaponData.Upgrade2)
        {
            upgrade2Button.interactable = false;
            upgrade2CostDisplay.text = "MAX";
        }
        else
        {
            upgrade2Cost = weapon.Upgrade2Cost[weaponData.Upgrade2];
            upgrade2CostDisplay.text = upgrade2Cost.ToString();
        }


        float _currentUpgrade = weaponData.Upgrade1 + weaponData.Upgrade2;
        float _upgradeCounter = (_currentUpgrade + 0.5f) / 2;
        _upgradeCounter = Mathf.Clamp(_upgradeCounter, 0, upgradeDisplays.Length);

        for (int i = 0; i < _upgradeCounter; i++)
        {
            upgradeDisplays[i].SetActive(true);
        }

        lockedHolder.SetActive(false);
        unlockedHolder.SetActive(true);
    }
}
