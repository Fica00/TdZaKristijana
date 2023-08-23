using UnityEngine;
using UnityEngine.UI;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] Sprite on;
    [SerializeField] Sprite off;
    [SerializeField] Image statusDisplay;
    [SerializeField] Button clickHandler;

    private void OnEnable()
    {
        clickHandler.onClick.AddListener(ToggleMusic);
    }

    private void OnDisable()
    {
        clickHandler.onClick.RemoveListener(ToggleMusic);
    }

    private void Start()
    {
        ShowStatus();
    }

    public void ToggleMusic()
    {
        DataManager.Instance.PlayerData.PlayMusic = !DataManager.Instance.PlayerData.PlayMusic;
        ShowStatus();
    }

    void ShowStatus()
    {
        statusDisplay.sprite = DataManager.Instance.PlayerData.PlayMusic ? on : off;
    }
}
