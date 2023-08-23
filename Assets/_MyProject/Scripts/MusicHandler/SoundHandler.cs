using UnityEngine;
using UnityEngine.UI;

public class SoundHandler : MonoBehaviour
{
    [SerializeField] Sprite on;
    [SerializeField] Sprite off;
    [SerializeField] Image statusDisplay;
    [SerializeField] Button clickHandler;

    private void OnEnable()
    {
        ShowStatus();
        clickHandler.onClick.AddListener(ToggleSOund);
    }

    private void OnDisable()
    {
        clickHandler.onClick.RemoveListener(ToggleSOund);
    }

    public void ToggleSOund()
    {
        DataManager.Instance.PlayerData.PlaySoundEffect = !DataManager.Instance.PlayerData.PlaySoundEffect;
        ShowStatus();
    }

    void ShowStatus()
    {
        statusDisplay.sprite = DataManager.Instance.PlayerData.PlaySoundEffect ? on : off;
    }
}
