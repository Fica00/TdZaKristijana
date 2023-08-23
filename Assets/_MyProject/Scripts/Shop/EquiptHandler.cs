using UnityEngine;
using UnityEngine.UI;


public class EquiptHandler : MonoBehaviour
{
    [SerializeField] Image[] weapondDisplays;
    [SerializeField] Sprite transparentSprite;
    [SerializeField] Button closeButton;
    [SerializeField] GameObject holder;
    GunSO gun;

    private void OnEnable()
    {
        WeaponUpgrade.Equipt += Setup;
        closeButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        WeaponUpgrade.Equipt -= Setup;
        closeButton.onClick.AddListener(Close);
    }

    void Setup(GunSO _gun)
    {
        if (DataManager.Instance.PlayerData.SelectedGuns.Contains(_gun.Id))
        {
            UIManager.Instance.OkDialog.Show("This weapon is already equipted");
            return;
        }

        for (int i = 0; i < weapondDisplays.Length; i++)
        {
            if (DataManager.Instance.PlayerData.SelectedGuns[i] != -1)
            {
                weapondDisplays[i].sprite = GunSO.Get(DataManager.Instance.PlayerData.SelectedGuns[i]).Sprite;
            }
            else
            {
                weapondDisplays[i].sprite = transparentSprite;
            }
        }
        gun = _gun;
        holder.SetActive(true);
    }

    //called from UIButtons
    public void Equipt(int _placeId)
    {
        DataManager.Instance.PlayerData.SelectWeapon(_placeId, gun.Id);
        Close();
    }

    void Close()
    {
        holder.SetActive(false);
    }

}
