using UnityEngine;
using TMPro;

public class AmmoDisplayHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bulletsDisplay;

    private void OnEnable()
    {
        GunController.UpdatedAmmo += ShowBulletsAmount;
    }

    private void OnDisable()
    {
        GunController.UpdatedAmmo -= ShowBulletsAmount;
    }

    public void ShowBulletsAmount(int _amount)
    {
        bulletsDisplay.text = _amount.ToString();
    }
}
